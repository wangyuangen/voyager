using YK.Console.Business.ApiInfos;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 接口服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup), Order(30)]
public class ApiInfoService(ISender _sender) : IDynamicApi
{
    /// <summary>
    /// 同步接口
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> SyncAsync(CancellationToken cancellationToken)
        => _sender.Send(new SyncApiFromSwaggerRequest());

    /// <summary>
    /// 变更接口状态
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> ChangeStatusAsync(ApiInfoEnabledStatusChangeRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 接口列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<ApiInfoOutput>> ListAsync(ApiInfoSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);
}
