using YK.Core.Enums;

namespace YK.Authorize.Models;

public class UserClaimInfo
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 用户账户名
    /// </summary>
    public string UserAccount { get; set; } = default!;

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string NickName { get; set; } = default!;

    /// <summary>
    /// 职工Id
    /// </summary>
    public Guid? UserStaffId { get; set; }

    /// <summary>
    /// 职工岗位Id
    /// </summary>
    public Guid? PostId { get; set; }

    /// <summary>
    /// 职工真实姓名
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 职工所属租户Id
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// 职工所属租户类型
    /// </summary>
    public TenantTypeEnum? TenantType { get; set; }

    /// <summary>
    /// 职工类型
    /// </summary>
    public UserStaffTypeEnum? UserStaffType { get; set; }

    /// <summary>
    /// 机构Id
    /// </summary>
    public Guid? OrgId { get; set; }

}
