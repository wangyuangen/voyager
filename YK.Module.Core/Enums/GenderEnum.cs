using System.ComponentModel;
using YK.Core.Attributes;

namespace YK.Module.Core.Enums;

/// <summary>
/// 性别
/// </summary>
public enum GenderEnum
{
    /// <summary>
    /// 未知
    /// </summary>
    [EnumTheme("info")]
    [Description("未知")]
    Unknown = 0,

    /// <summary>
    /// 男
    /// </summary>
    [EnumTheme("warning")]
    [Description("男")]
    Male = 1,

    /// <summary>
    /// 女
    /// </summary>
    [EnumTheme("success")]
    [Description("女")]
    Female = 2
}
