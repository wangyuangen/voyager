using YK.Console.Business.Abstractions;
using YK.Console.Core.Consts;
using YK.Notice.Models;
using YK.ORM.Contract;

namespace YK.Console;

/// <summary>
/// 在线用户服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup),Order(105)]
public class OnlineUserService(IOnlineUser _onlineUser):IDynamicApi
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task<PaginationResponse<OnlineUserInfo>> PageAsync(PaginationFilter filter, CancellationToken cancellationToken)
        => _onlineUser.PageAsync(filter, cancellationToken);

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ForceOfflineAsync([Required]string connectionId,CancellationToken cancellationToken)
        =>_onlineUser.ForceOfflineAsync(connectionId, cancellationToken);
}
