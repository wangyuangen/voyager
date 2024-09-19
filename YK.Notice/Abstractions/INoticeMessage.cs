using YK.Notice.Models;

namespace YK.Notice.Abstractions;

/// <summary>
/// 实时消息推送
/// </summary>
public interface INoticeMessage
{
    /// <summary>
    /// 发送给所有用户
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    Task SendToAllAsync(NoticeMessageContent content);

    /// <summary>
    /// 发送给指定用户
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToUserAsync(NoticeMessageContent content,CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送给多个用户
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToUsersAsync(NoticeMessageContent content, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送给指定组
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    Task SendToGroupAsync(NoticeMessageContent content);

    /// <summary>
    /// 发送给多个组
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    Task SendToGroupsAsync(NoticeMessageContent content);

    /// <summary>
    /// 发送给指定员工
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToStaffAsync(NoticeMessageContent content, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送给多个员工
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendToStaffsAsync(NoticeMessageContent content, CancellationToken cancellationToken = default);
}
