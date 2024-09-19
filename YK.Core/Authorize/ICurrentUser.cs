using System.Security.Claims;
using YK.Core.Enums;

namespace YK.Core.Authorize;

/// <summary>
/// 当前用户
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 用户Id
    /// </summary>
    Guid UserId { get; }

    /// <summary>
    /// 用户昵称
    /// </summary>
    string NickName { get; }

    /// <summary>
    /// 用户账户
    /// </summary>
    string UserAccount { get; }

    /// <summary>
    /// 职工所属租户Id
    /// </summary>
    Guid? TenantId { get; }

    /// <summary>
    /// 职工所属租户类型
    /// </summary>
    TenantTypeEnum? TenantType { get; }

    /// <summary>
    /// 职工Id
    /// </summary>
    Guid? UserStaffId { get; }

    /// <summary>
    /// 职工岗位Id
    /// </summary>
    Guid? PostId { get; }

    /// <summary>
    /// 职工真实姓名
    /// </summary>
    string? RealName { get; }

    /// <summary>
    /// 职工类型
    /// </summary>
    UserStaffTypeEnum? UserStaffType { get; }

    /// <summary>
    /// 身份声明信息
    /// </summary>
    IEnumerable<Claim>? UserClaims { get; }

    /// <summary>
    /// 机构Id
    /// </summary>
    Guid? OrgId { get; }
}
