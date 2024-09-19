using YK.Notice.Models;

namespace YK.Notice.Abstractions;

/// <summary>
/// 集线器
/// </summary>
public interface INotificationHub
{
    /// <summary>
    /// 在线用户列表
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task OnlineUserList(OnlineUserChangeNotice context);

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ForceOffline(object context);

    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ReceiveMessage(object context);
}
