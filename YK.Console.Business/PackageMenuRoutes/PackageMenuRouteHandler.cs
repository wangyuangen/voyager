using Microsoft.EntityFrameworkCore;

namespace YK.Console.Business.PackageMenuRoutes;

internal class SavePackageMenuRouteHandler(IRepository<PackageMenuRoute> _repo) : IRequestHandler<SavePackageMenuRouteRequest, bool>
{
    public async Task<bool> Handle(SavePackageMenuRouteRequest request, CancellationToken cancellationToken)
    {
        var exists = await _repo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.PackageId == request.PackageId)
            .Select(x => x.MenuRouteId)
            .ToListAsync();

        var inserts = request.MenuRouteIds.Except(exists);

        if (inserts.Any())
        {
            //保存
            var pkgMenuRoutes = inserts.Select(x => new PackageMenuRoute
            {
                MenuRouteId = x,
                PackageId = request.PackageId,
            }).ToList();

            await _repo.AddRangeAsync(pkgMenuRoutes, cancellationToken);
        }

        var deletes = exists.Except(request.MenuRouteIds);

        if (deletes.Any())
        {
            await _repo.SoftDeleteAsync(x => x.PackageId == request.PackageId && deletes.Contains(x.MenuRouteId), cancellationToken);
        }

        return true;
    }
}

internal class PackageMenuRouteSearchByPackagesHandler(IReadRepository<PackageMenuRoute> _repo) : IRequestHandler<PackageMenuRouteSearchByPackagesRequest, List<PackageMenuRouteOutput>>
{
    public Task<List<PackageMenuRouteOutput>> Handle(PackageMenuRouteSearchByPackagesRequest request, CancellationToken cancellationToken)
        => _repo.SimpleListAsync<PackageMenuRouteOutput>(x => request.PackageIds.Contains(x.PackageId) && x.MenuRoute.Enabled == EnabledStatusEnum.Enabled, cancellationToken);
}
