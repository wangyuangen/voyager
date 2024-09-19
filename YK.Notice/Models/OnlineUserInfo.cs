namespace YK.Notice.Models;

/// <summary>
/// 在线用户信息
/// </summary>
public class OnlineUserInfo
{
    /// <summary>
    /// 连接Id
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 员工Id
    /// </summary>
    public Guid? UserStaffId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 连接时间
    /// </summary>
    public DateTime ConnectionTime { get; set; }

    /// <summary>
    /// 连接IP
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public Guid? TenantId { get; set; }
}
