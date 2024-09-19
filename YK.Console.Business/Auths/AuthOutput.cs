namespace YK.Console.Business.Auths;

/// <summary>
/// 当前登录用户身份信息
/// </summary>
public class CurrentUserStaffOutput
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 职工所属租户Id
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// 职工所属租户类型
    /// </summary>
    public TenantTypeEnum TenantType { get; set; }

    /// <summary>
    /// 职工Id
    /// </summary>
    public Guid UserStaffId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 职工类型
    /// </summary>
    public UserStaffTypeEnum UserStaffType { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// 接口权限范围
    /// </summary>
    public List<string> ApiPermissionScope { get; set; }
}
