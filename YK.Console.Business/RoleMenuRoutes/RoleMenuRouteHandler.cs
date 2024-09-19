using Microsoft.EntityFrameworkCore;

namespace YK.Console.Business.RoleMenuRoutes;

internal class SaveRoleMenuRouteHandler(IRepository<RoleMenuRoute> _repo) : IRequestHandler<SaveRoleMenuRouteRequest, bool>
{
    public async Task<bool> Handle(SaveRoleMenuRouteRequest request, CancellationToken cancellationToken)
    {
        var roleMenus = await _repo.NoDataPermissionQueryable().AsNoTracking()
             .Where(x => x.RoleId == request.RoleId)
             .Select(x => x.MenuRouteId)
            .ToListAsync();

        var insertMenus = request.MenuRouteIds.Except(roleMenus);

        if (insertMenus.Any())
        {
            var roleMenuRoutes = insertMenus.Select(x => new RoleMenuRoute
            {
                MenuRouteId = x,
                RoleId = request.RoleId
            }).ToList();

            await _repo.AddRangeAsync(roleMenuRoutes, cancellationToken);
        }

        var deleteMenus = roleMenus.Except(request.MenuRouteIds);

        if (deleteMenus.Any())
        {
            await _repo.DeleteAsync(x => x.RoleId == request.RoleId && deleteMenus.Contains(x.MenuRouteId), cancellationToken);
        }

        return true;
    }
}
