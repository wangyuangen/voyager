using Microsoft.EntityFrameworkCore;

namespace YK.Console.Business.RolePermissionGroups;

internal class SaveRolePermissionGroupHandler(IRepository<RolePermissionGroup> _repo) : IRequestHandler<SaveRolePermissionGroupRequest, bool>
{
    public async Task<bool> Handle(SaveRolePermissionGroupRequest request, CancellationToken cancellationToken)
    {
        var rolePermissions = await _repo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.RoleId == request.RoleId)
            .Select(x => x.PermissionGroupId)
            .ToListAsync();

        var insertPermissionGroups = request.PermissionGroupIds.Except(rolePermissions);

        if (insertPermissionGroups.Any())
        {
            var rolePermissionGourps = insertPermissionGroups.Select(x => new RolePermissionGroup
            {
                RoleId = request.RoleId,
                PermissionGroupId = x
            }).ToList();

            await _repo.AddRangeAsync(rolePermissionGourps, cancellationToken);
        }

        var deletePermissionGroups = rolePermissions.Except(request.PermissionGroupIds);

        if (deletePermissionGroups.Any())
        {
            await _repo.SoftDeleteAsync(x => x.RoleId == request.RoleId && deletePermissionGroups.Contains(x.PermissionGroupId), cancellationToken);
        }

        return true;
    }
}
