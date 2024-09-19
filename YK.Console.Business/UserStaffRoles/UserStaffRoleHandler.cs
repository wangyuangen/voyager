using Microsoft.EntityFrameworkCore;
using YK.Cache.Abstractions;
using YK.Cache.Consts;
using YK.Console.Business.PermissionGroupApis;

namespace YK.Console.Business.UserStaffRoles;

internal class SaveUserStaffRoleHandler(IRepository<UserStaffRole> _repo,ICacheManager _cache,ICurrentUser _currentUser) : IRequestHandler<SaveUserStaffRoleRequest, bool>
{
    public async Task<bool> Handle(SaveUserStaffRoleRequest request, CancellationToken cancellationToken)
    {
        var exists = await _repo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.UserStaffId == request.UserStaffId)
            .Select(x => x.RoleId)
            .ToListAsync();

        var inserts = request.RoleIds.Except(exists);

        if (inserts.Any())
        {
            //保存
            var staffRoles = inserts.Select(x => new UserStaffRole
            {
                RoleId = x,
                UserStaffId = request.UserStaffId,
            }).ToList();

            await _repo.AddRangeAsync(staffRoles, cancellationToken);
        }

        var deletes = exists.Except(request.RoleIds);

        if (deletes.Any())
        {
            await _repo.DeleteAsync(x => x.UserStaffId == request.UserStaffId && deletes.Contains(x.RoleId), cancellationToken);
        }

        var key = CacheConsts.GetKey<ApiPermissionDataScope>(_currentUser.TenantId, request.UserStaffId);
        await _cache.RemoveAsync(key, cancellationToken);

        return true;
    }
}
