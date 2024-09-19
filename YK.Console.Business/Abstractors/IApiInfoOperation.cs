using YK.Console.Business.ApiInfos;

namespace YK.Console.Business.Abstractors;

/// <summary>
/// 接口
/// </summary>
[UnitOfWork]
public interface IApiInfoOperation:IScopedService
{
    /// <summary>
    /// 接口同步
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UnitOfWork]
    Task<bool> SyncAsync(SyncApiFromSwaggerRequest request, CancellationToken cancellationToken);
}
