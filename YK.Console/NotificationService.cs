using YK.Console.Core.Consts;
using YK.Notice.Abstractions;
using YK.Notice.Models;

namespace YK.Console;

/// <summary>
/// 实时消息推送服务
/// </summary>
[DynamicApi(Area =ConsoleAppConsts.ApiGroup),Order(107)]
public class NotificationService(INoticeMessage _notice):IDynamicApi
{
    /// <summary>
    /// 发送给所有用户
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public Task SendToAllAsync(NoticeMessageContent content)
        =>_notice.SendToAllAsync(content);

    /// <summary>
    /// 发送给指定用户
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SendToUserAsync(NoticeMessageContent content, CancellationToken cancellationToken)
        =>_notice.SendToUserAsync(content, cancellationToken);

    /// <summary>
    /// 发送给多个用户
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SendToUsersAsync(NoticeMessageContent content, CancellationToken cancellationToken)
        =>_notice.SendToUsersAsync(content, cancellationToken);

    /// <summary>
    /// 发送给指定组
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public Task SendToGroupAsync(NoticeMessageContent content)
        =>_notice.SendToGroupAsync(content);

    /// <summary>
    /// 发送给多个组
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public Task SendToGroupsAsync(NoticeMessageContent content)
        =>_notice.SendToGroupsAsync(content);

    /// <summary>
    /// 发送给指定员工
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SendToStaffAsync(NoticeMessageContent content, CancellationToken cancellationToken)
        =>_notice.SendToStaffAsync(content, cancellationToken);

    /// <summary>
    /// 发送给多个员工
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SendToStaffsAsync(NoticeMessageContent content, CancellationToken cancellationToken)
        => _notice.SendToStaffsAsync(content,cancellationToken);
}
