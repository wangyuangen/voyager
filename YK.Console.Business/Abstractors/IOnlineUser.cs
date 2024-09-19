using YK.Notice.Models;

namespace YK.Console.Business.Abstractions;

/// <summary>
/// 在线用户
/// </summary>
public interface IOnlineUser:IScopedService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PaginationResponse<OnlineUserInfo>> PageAsync(PaginationFilter filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ForceOfflineAsync(string connectionId, CancellationToken cancellationToken = default);
}
