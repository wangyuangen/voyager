using Microsoft.EntityFrameworkCore;
using YK.Cache;
using YK.Cache.Abstractions;
using YK.Cache.Consts;

namespace YK.Console.Business.PermissionGroupApis;

internal class SavePermissionGroupApiHandler(IRepository<PermissionGroupApi> _repo) : IRequestHandler<SavePermissionGroupApiRequest, bool>
{
    public async Task<bool> Handle(SavePermissionGroupApiRequest request, CancellationToken cancellationToken)
    {
        var permissionApis = await _repo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.PermissionGroupId == request.PermissionGroupId)
            .Select(x => x.ApiId)
            .ToListAsync(cancellationToken);

        var inserts = request.ApiIds.Except(permissionApis);

        if (inserts.Any())
        {
            var insertApis = inserts.Select(x => new PermissionGroupApi
            {
                PermissionGroupId = request.PermissionGroupId,
                ApiId = x
            }).ToList();
            await _repo.AddRangeAsync(insertApis, cancellationToken);
        }

        var deletes = permissionApis.Except(request.ApiIds);

        if (deletes.Any())
        {
            await _repo.SoftDeleteAsync(x => x.PermissionGroupId == request.PermissionGroupId && deletes.Contains(x.ApiId), cancellationToken);
        }    
       
        return true;
    }
}

internal class CurrentUserApiPermissionDataScopeHandler(IReadRepository<UserStaffRole> _userStaffRoleRepo, ICurrentUser _currentUser,ICacheManager _cache)
    : IRequestHandler<CurrentUserApiPermissionDataScopeRequest, List<ApiPermissionDataScope>?>
{
    public async Task<List<ApiPermissionDataScope>?> Handle(CurrentUserApiPermissionDataScopeRequest request, CancellationToken cancellationToken)
    {
        if (_currentUser.UserStaffId.IsNullOrEmpty()) return null;

        var currentUserStaffId = _currentUser.UserStaffId ?? Guid.Empty;

        var key = CacheConsts.GetKey<ApiPermissionDataScope>(_currentUser.TenantId, currentUserStaffId);

        var apiPermissionScopes = await _cache.GetOrSetAsync(key, async () =>
        {
            var userStaffRoles = await _userStaffRoleRepo.NoDataPermissionQueryable().AsNoTracking()
                .Where(x => x.UserStaffId == currentUserStaffId)
                .Include(x => x.Role.RolePermissionGroups.Where(x => x.PermissionGroup.Enabled == EnabledStatusEnum.Enabled))
                .ThenInclude(x => x.PermissionGroup.PermissionGroupApis.Where(x => x.Api.Enabled == EnabledStatusEnum.Enabled))
                .ThenInclude(x=>x.Api)
                .ToListAsync(cancellationToken);

            var result = userStaffRoles.SelectMany(x =>
                x.Role.RolePermissionGroups.SelectMany(rp =>
                    rp.PermissionGroup.PermissionGroupApis.Select(pg =>
                        new ApiPermissionDataScope(rp.PermissionGroup.Scope, pg.Api?.HttpMethod ?? string.Empty, pg.Api?.Path ?? string.Empty, pg.Api?.Code ?? string.Empty)
            )));

            return result.Distinct().ToList();

        }, cancellationToken: cancellationToken);

        return apiPermissionScopes;
    }
}
