using System.ComponentModel;

namespace YK.Core.Enums;

/// <summary>
/// 职工类型
/// </summary>
[Description("职工类型")]
public enum UserStaffTypeEnum
{
    /// <summary>
    /// 普通员工
    /// </summary>
    [Description("普通职工")]
    Normal = 1,

    /// <summary>
    /// 租户管理员
    /// </summary>
    [Description("租户管理员")]
    TenantAdmin = 10,

    /// <summary>
    /// 平台管理员
    /// </summary>
    [Description("平台管理员")]
    PlatformAdmin = 100
}
