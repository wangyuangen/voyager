using YK.Console.Business.Packages;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 套餐服务
/// </summary>
/// <param name="_sender"></param>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup), Order(60)]
public class PackageInfoService(ISender _sender) : IDynamicApi
{
    /// <summary>
    /// 创建套餐
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreatePackageInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑套餐
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdatePackageInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 删除套餐
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeletePackageInfoRequest(id), cancellationToken);

    /// <summary>
    /// 套餐列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<PackageInfoOutput>> ListAsync(PackageInfoSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 套餐分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<PackageInfoOutput>> PageAsync(PackageInfoPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 套餐授权变更
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> AuthChangeAsync(PackageAuthChangeRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 获取套餐授权信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PackageAuthInfoOutput> AuthInfoAsync([Required] Guid id, CancellationToken cancellationToken)
        => _sender.Send(new GetPackageAuthInfoRequest(id), cancellationToken);

}
