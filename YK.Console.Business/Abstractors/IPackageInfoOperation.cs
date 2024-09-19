using YK.Console.Business.Packages;

namespace YK.Console.Business.Abstractors;

/// <summary>
/// 套餐
/// </summary>
[UnitOfWork]
public interface IPackageInfoOperation:IScopedService
{
    /// <summary>
    /// 套餐授权变更
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UnitOfWork]
    Task<Guid> AuthChangeAsync(PackageAuthChangeRequest request, CancellationToken cancellationToken);
}
