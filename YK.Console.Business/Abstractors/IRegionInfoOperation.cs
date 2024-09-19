using YK.Console.Core.Enums;

namespace YK.Console.Business.Abstractors;

/// <summary>
/// 地区
/// </summary>
[UnitOfWork]
public interface IRegionInfoOperation:IScopedService
{
    /// <summary>
    /// 同步
    /// </summary>
    /// <param name="regionLevel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UnitOfWork]
    Task SyncAsync(RegionLevel regionLevel = RegionLevel.City, CancellationToken cancellationToken = default);
}
