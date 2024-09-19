using YK.Notice.Enums;

namespace YK.Notice.Models;

/// <summary>
/// 消息内容
/// </summary>
public class NoticeMessageContent
{
    /// <summary>
    /// 消息接收对象
    /// </summary>
    public Guid? Receiver { get; set; }

    /// <summary>
    /// 消息接收对象集合
    /// </summary>
    public List<Guid>? Receivers { get; set; }

    /// <summary>
    /// 消息标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public NoticeMessageType MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; }
}
