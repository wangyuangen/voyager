using Microsoft.AspNetCore.SignalR;
using YK.Cache.Abstractions;
using YK.Cache.Consts;
using YK.Core.Extensions;
using YK.Notice.Abstractions;
using YK.Notice.Consts;
using YK.Notice.Models;

namespace YK.Notice;

public class NoticeMessage(ICacheManager _cache, IHubContext<NotificationHub, INotificationHub> _hub) : INoticeMessage
{
    /// <summary>
    /// 发送给所有用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task SendToAllAsync(NoticeMessageContent content)
    {
        await _hub.Clients.All.ReceiveMessage(content);
    }

    /// <summary>
    /// 发送给指定组
    /// </summary>
    /// <param name="content">Receiver为TenantId</param>
    /// <returns></returns>
    public async Task SendToGroupAsync(NoticeMessageContent content)
    {
        if (!content.Receiver.HasValue) return;

        var groupName = NoticeConsts.TenantGroupName(content.Receiver);

        await _hub.Clients.Group(groupName).ReceiveMessage(content);
    }

    /// <summary>
    /// 发送给多个组
    /// </summary>
    /// <param name="content">Receivers为TenantId集合</param>
    /// <returns></returns>
    public async Task SendToGroupsAsync(NoticeMessageContent content)
    {
        if (content.Receivers.IsNullOrEmpty())
            return;

        List<string> groupNames = new();
        content.Receivers?.ForEach(tenantId =>
        {
            groupNames.Add(NoticeConsts.TenantGroupName(tenantId));
        });

        await _hub.Clients.Groups(groupNames).ReceiveMessage(content);
    }

    /// <summary>
    /// 发送给指定员工
    /// </summary>
    /// <param name="content">Receiver为UserStaffId</param>
    /// <returns></returns>
    public async Task SendToStaffAsync(NoticeMessageContent content, CancellationToken cancellationToken = default)
    {
        if (!content.Receiver.HasValue) return;

        var cacheKey = CacheConsts.GetKey<OnlineUserInfo>();
        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cacheKey, cancellationToken);

        if (onlineUsers.IsNullOrEmpty()) return;

        onlineUsers = onlineUsers?.Where(x => x.UserStaffId == content.Receiver).ToList();

        var connectionIds = onlineUsers?.Select(x => x.ConnectionId).ToList();

        await _hub.Clients.Clients(connectionIds).ReceiveMessage(content);
    }

    /// <summary>
    /// 发送给多个员工
    /// </summary>
    /// <param name="content">Receivers为UserStaffId集合</param>
    /// <returns></returns>
    public async Task SendToStaffsAsync(NoticeMessageContent content, CancellationToken cancellationToken = default)
    {
        if (content.Receivers.IsNullOrEmpty())
            return;

        var cacheKey = CacheConsts.GetKey<OnlineUserInfo>();
        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cacheKey, cancellationToken);

        if (onlineUsers.IsNullOrEmpty()) return;

        onlineUsers = onlineUsers?.Where(x => content.Receivers?.Contains(x.UserStaffId??Guid.Empty) is true).ToList();

        var connectionIds = onlineUsers?.Select(x => x.ConnectionId).ToList();

        await _hub.Clients.Clients(connectionIds).ReceiveMessage(content);
    }

    /// <summary>
    /// 发送给指定用户
    /// </summary>
    /// <param name="content">Receiver为UserId</param>
    /// <returns></returns>
    public async Task SendToUserAsync(NoticeMessageContent content, CancellationToken cancellationToken = default)
    {
        if (!content.Receiver.HasValue) return;

        var cacheKey = CacheConsts.GetKey<OnlineUserInfo>();
        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cacheKey, cancellationToken);

        if (onlineUsers.IsNullOrEmpty()) return;

        onlineUsers = onlineUsers?.Where(x => x.UserId == content.Receiver).ToList();

        var connectionIds = onlineUsers?.Select(x => x.ConnectionId).ToList();

        await _hub.Clients.Clients(connectionIds).ReceiveMessage(content);
    }

    /// <summary>
    /// 发送给多个用户
    /// </summary>
    /// <param name="content">Receivers为UserId集合</param>
    /// <returns></returns>
    public async Task SendToUsersAsync(NoticeMessageContent content, CancellationToken cancellationToken = default)
    {
        if (content.Receivers.IsNullOrEmpty())
            return;

        var cacheKey = CacheConsts.GetKey<OnlineUserInfo>();
        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cacheKey, cancellationToken);

        if (onlineUsers.IsNullOrEmpty()) return;

        onlineUsers = onlineUsers?.Where(x => content.Receivers?.Contains(x.UserId) is true).ToList();

        var connectionIds = onlineUsers?.Select(x => x.ConnectionId).ToList();

        await _hub.Clients.Clients(connectionIds).ReceiveMessage(content);
    }
}
