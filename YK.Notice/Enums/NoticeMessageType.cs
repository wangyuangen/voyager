using System.ComponentModel;

namespace YK.Notice.Enums;

/// <summary>
/// 通知消息类型
/// </summary>
[Description("通知消息类型")]
public enum NoticeMessageType
{
    /// <summary>
    /// 普通信息
    /// </summary>
    [Description("消息")]
    Info = 0,

    /// <summary>
    /// 成功提示
    /// </summary>
    [Description("成功")]
    Success = 1,

    /// <summary>
    /// 警告提示
    /// </summary>
    [Description("警告")]
    Warning = 2,

    /// <summary>
    /// 错误提示
    /// </summary>
    [Description("错误")]
    Error = 3
}
