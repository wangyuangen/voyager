using Microsoft.EntityFrameworkCore;
using YK.Console.Business.Tenants;
using YK.Console.Core.Consts;
using YK.Console.Core.Entities;
using YK.ORM.Abstractions;

namespace YK.Console;

/// <summary>
/// 租户服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup), Order(20)]
public class TenantService(ISender _sender, IReadRepository<TenantPackage> _tpRepo) : IDynamicApi
{
    /// <summary>
    /// 创建租户
    /// </summary>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateTenantRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑租户
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateTenantRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 租户分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<TenantOutput>> PageAsync(TenantPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 获取租户套餐
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Guid>> PackagesAsync([Required] Guid id, CancellationToken cancellationToken)
        => _tpRepo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.TenantId == id)
            .Select(x => x.PackageId)
            .ToListAsync(cancellationToken);
}
