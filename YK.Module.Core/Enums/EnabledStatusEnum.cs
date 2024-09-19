using System.ComponentModel;
using YK.Core.Attributes;

namespace YK.Module.Core.Enums;

/// <summary>
/// 启用状态
/// </summary>
[Description("启用状态")]
public enum EnabledStatusEnum
{
    /// <summary>
    /// 禁用
    /// </summary>
    [EnumTheme("info")]
    [Description("禁用")]
    Disabled = 0,

    /// <summary>
    /// 启用
    /// </summary>
    [EnumTheme("success")]
    [Description("启用")]
    Enabled = 1,
}
