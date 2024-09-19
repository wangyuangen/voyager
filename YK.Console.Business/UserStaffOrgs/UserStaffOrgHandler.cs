using Microsoft.EntityFrameworkCore;
using YK.Cache;
using YK.Cache.Abstractions;
using YK.Cache.Consts;
using YK.Console.Business.OrganizeInfos;

namespace YK.Console.Business.UserStaffOrgs;

internal class SaveUserStaffOrgHandler(IRepository<UserStaffOrg> _repo,ICacheManager _cache,ICurrentUser _currentUser) : IRequestHandler<SaveUserStaffOrgRequest, bool>
{
    public async Task<bool> Handle(SaveUserStaffOrgRequest request, CancellationToken cancellationToken)
    {
        var exists = await _repo.NoDataPermissionQueryable().AsNoTracking()
             .Where(x => x.UserStaffId == request.UserStaffId)
             .Select(x => x.OrgId)
             .ToListAsync();

        var inserts = request.OrgIds.Except(exists);

        if (inserts.Any())
        {
            //保存
            var staffOrgs = inserts.Select(x => new UserStaffOrg
            {
                OrgId = x,
                UserStaffId = request.UserStaffId,
            }).ToList();

            await _repo.AddRangeAsync(staffOrgs, cancellationToken);
        }

        var deletes = exists.Except(request.OrgIds);

        if (deletes.Any())
        {
            await _repo.DeleteAsync(x => x.UserStaffId == request.UserStaffId && deletes.Contains(x.OrgId), cancellationToken);
        }

        var key = CacheConsts.GetKey<UserStaffPermissionScope>(_currentUser.TenantId, request.UserStaffId);
        await _cache.RemoveAsync(key, cancellationToken);

        return true;
    }
}

internal class GetUserStaffOrgsByPermissionScopeHandler(ICurrentUser _currentUser,ICacheManager _cache,IReadRepository<UserStaffOrg> _repo,ISender _sender)
    : IRequestHandler<GetUserStaffOrgsByPermissionScopeRequest, UserStaffPermissionScope?>
{
    public async Task<UserStaffPermissionScope?> Handle(GetUserStaffOrgsByPermissionScopeRequest request, CancellationToken cancellationToken)
    {
        var permissionScopes = await GetOrgPermissionScopesAsync(cancellationToken);

        var result = permissionScopes?.FirstOrDefault(x => x.PermissionScope == request.PermissionScope);

        return result;
    }

    private async Task<List<UserStaffPermissionScope>?> GetOrgPermissionScopesAsync(CancellationToken cancellationToken)
    {
        if (_currentUser.UserStaffId.IsNullOrEmpty()) return null;

        var currentUserStaffId = _currentUser.UserStaffId ?? Guid.Empty;

        var key = CacheConsts.GetKey<UserStaffPermissionScope>(_currentUser.TenantId, currentUserStaffId);

        var permissionScopes = await _cache.GetOrSetAsync(key, async () =>
        {
            var orgPermissionScopes = new List<UserStaffPermissionScope>();
            //附属部门
            var affiliateOrgs = await _repo.NoDataPermissionQueryable()
                .Where(x => x.UserStaffId == currentUserStaffId)
                .Select(x => x.OrgId)
                .ToListAsync();

            var affiliateWithChild = affiliateOrgs;

            if (_currentUser.OrgId.HasValue)
            {
                var parentOrgId = _currentUser.OrgId ?? Guid.Empty;

                //获取子级部门
                var childOrgs = await _sender.Send(new GetChildOrgsByParentRequest(parentOrgId), cancellationToken);

                var childOrgIds = childOrgs.Select(x => x.Id).ToList();

                //本部门和下级部门
                childOrgIds.Add(parentOrgId);

                //本部门和附属部门
                affiliateOrgs.Add(parentOrgId);

                //本部门和下级部门以及附属部门
                affiliateWithChild.AddRange(childOrgIds);

                orgPermissionScopes.Add(new UserStaffPermissionScope(childOrgIds.Distinct().ToList(), DataPermissionScope.CurrentWithChildDept));
            }
            orgPermissionScopes.Add(new UserStaffPermissionScope(affiliateOrgs.Distinct().ToList(), DataPermissionScope.CurrentWithAffiliateDept));

            orgPermissionScopes.Add(new UserStaffPermissionScope(affiliateWithChild.Distinct().ToList(), DataPermissionScope.CurrentWithChildAndAffiliateDept));

            return orgPermissionScopes;
        });

        return permissionScopes;
    }
}
