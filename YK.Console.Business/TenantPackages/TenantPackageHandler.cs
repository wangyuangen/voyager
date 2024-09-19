using Microsoft.EntityFrameworkCore;

namespace YK.Console.Business.TenantPackages;

internal class SaveTenantPackageHandler(IRepository<TenantPackage> _repo) : IRequestHandler<SaveTenantPackageRequest, bool>
{
    public async Task<bool> Handle(SaveTenantPackageRequest request, CancellationToken cancellationToken)
    {
        var exists = await _repo.NoDataPermissionQueryable().AsNoTracking()
           .Where(x => x.TenantId == request.TenantId)
           .Select(x => x.PackageId)
           .ToListAsync();

        var inserts = request.PackageIds.Except(exists);

        if (inserts.Any())
        {
            //保存
            var tenantPackages = inserts.Select(x => new TenantPackage
            {
                PackageId = x,
                TenantId = request.TenantId,
            }).ToList();

            await _repo.AddRangeAsync(tenantPackages, cancellationToken);
        }

        var deletes = exists.Except(request.PackageIds);

        if (deletes.Any())
        {
            await _repo.SoftDeleteAsync(x => x.TenantId == request.TenantId && deletes.Contains(x.PackageId), cancellationToken);
        }

        return true;
    }
}

internal class TenantPackageSearchByTenantHandler(IReadRepository<TenantPackage> _repo) : IRequestHandler<TenantPackageSearchByTenantRequest, List<TenantPackageOutput>>
{
    public Task<List<TenantPackageOutput>> Handle(TenantPackageSearchByTenantRequest request, CancellationToken cancellationToken)
        => _repo.SimpleListAsync<TenantPackageOutput>(x => x.TenantId == request.TenantId, cancellationToken);
}
