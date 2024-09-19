namespace YK.Notice.Models;

/// <summary>
/// 在线用户状态变更通知
/// </summary>
public class OnlineUserChangeNotice
{
    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }
    /// <summary>
    /// 是否在线
    /// </summary>
    public bool Online { get; set; }
    /// <summary>
    /// 通知对象
    /// </summary>
    public List<OnlineUserInfo> NoticeUsers { get; set; }
}
