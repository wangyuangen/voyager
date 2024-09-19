using System.ComponentModel;

namespace YK.Core.Enums;

/// <summary>
/// 租户类型
/// </summary>
[Description("租户类型")]
public enum TenantTypeEnum
{
    /// <summary>
    /// 普通租户
    /// </summary>
    [Description("普通租户")]
    NormalTenant = 0,

    /// <summary>
    /// 平台租户
    /// </summary>
    [Description("平台租户")]
    PlatformTenant = 1
}
