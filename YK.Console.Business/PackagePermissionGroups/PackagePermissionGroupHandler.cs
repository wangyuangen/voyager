using Microsoft.EntityFrameworkCore;

namespace YK.Console.Business.PackagePermissionGroups;

internal class SavePackagePermissionGroupHandler(IRepository<PackagePermissionGroup> _repo) : IRequestHandler<SavePackagePermissionGroupRequest, bool>
{
    public async Task<bool> Handle(SavePackagePermissionGroupRequest request, CancellationToken cancellationToken)
    {
        var exists = await _repo.NoDataPermissionQueryable().AsNoTracking()
           .Where(x => x.PackageId == request.PackageId)
           .Select(x => x.PermissionGroupId)
           .ToListAsync(cancellationToken);

        var inserts = request.PermissionGroupIds.Except(exists);

        if (inserts.Any())
        {
            //保存
            var pkgPermissionGourps = inserts.Select(x => new PackagePermissionGroup
            {
                PermissionGroupId = x,
                PackageId = request.PackageId,
            }).ToList();

            await _repo.AddRangeAsync(pkgPermissionGourps, cancellationToken);
        }

        var deletes = exists.Except(request.PermissionGroupIds);

        if (deletes.Any())
        {
            await _repo.SoftDeleteAsync(x => x.PackageId == request.PackageId && deletes.Contains(x.PermissionGroupId), cancellationToken);
        }

        return true;
    }
}


internal class PkgPermissionGroupSearchByPkgIdsHandler(IReadRepository<PackagePermissionGroup> _repo) 
    : IRequestHandler<PkgPermissionGroupSearchByPkgIdsRequest, List<PackagePermissionGroupOutput>>
{
    public Task<List<PackagePermissionGroupOutput>> Handle(PkgPermissionGroupSearchByPkgIdsRequest request, CancellationToken cancellationToken)
        => _repo.SimpleListAsync<PackagePermissionGroupOutput>(x => request.PackageIds.Contains(x.PackageId) && x.PermissionGroup.Enabled == EnabledStatusEnum.Enabled, cancellationToken);
}
