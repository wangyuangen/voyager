using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using UAParser;
using YK.Authorize.Abstractions;
using YK.Cache.Abstractions;
using YK.Cache.Consts;
using YK.Core.Consts;
using YK.Core.Extensions;
using YK.Notice.Abstractions;
using YK.Notice.Consts;
using YK.Notice.Models;

namespace YK.Notice;

[Authorize]
public class NotificationHub(ICacheManager _cache, IHubContext<NotificationHub,INotificationHub> _hub,ITokenBuilder _token) 
    : Hub<INotificationHub>
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();

        var token = httpContext?.Request.Query[QuerystringKeys.AccessToken];
        
        if (token.IsNull()) return;

        var tokenDecodeInfo = _token.DecodeToken(token.SafeString());

        if (tokenDecodeInfo.IsNull()) return;

        var client = Parser.GetDefault().Parse(httpContext?.Request.Headers["User-Agent"]);

        var cacheKey = CacheConsts.GetKey<OnlineUserInfo>();

        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cacheKey);

        onlineUsers = onlineUsers ?? new List<OnlineUserInfo>();

        var onlineUser = new OnlineUserInfo
        {
            ConnectionId = Context.ConnectionId,
            ConnectionTime = DateTime.Now,
            Ip = httpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "unknown",
            Browser = client.UA.Family + client.UA.Major,
            Os = client.OS.Family + client.OS.Major,
            TenantId = tokenDecodeInfo?.UserClaimInfo.TenantId,
            RealName = tokenDecodeInfo?.UserClaimInfo.RealName??string.Empty,
            UserId = tokenDecodeInfo?.UserClaimInfo.UserId ?? Guid.Empty,
            NickName = tokenDecodeInfo?.UserClaimInfo.NickName??string.Empty,
            UserStaffId = tokenDecodeInfo?.UserClaimInfo.UserStaffId
        };
        onlineUsers.Add(onlineUser);
        //写入在线用户缓存
        await _cache.SetAsync(cacheKey, onlineUsers);

        string groupName = NoticeConsts.TenantGroupName(onlineUser.TenantId);
        //将当前链接用户加入租户组
        await _hub.Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        onlineUsers = onlineUsers.Where(x => x.TenantId == onlineUser.TenantId).ToList();

        //向当前组发送上线通知
        await _hub.Clients.Groups(groupName).OnlineUserList(new OnlineUserChangeNotice
        {
            RealName = onlineUser.RealName,
            Online = true,
            NoticeUsers = onlineUsers
        });
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (string.IsNullOrEmpty(Context.ConnectionId)) return;

        var cacheKey = CacheConsts.GetKey<OnlineUserInfo>();

        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cacheKey);

        if (onlineUsers.IsNullOrEmpty()) return;

        var currentUser = onlineUsers?.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

        if (currentUser == null) return;

        onlineUsers?.Remove(currentUser);
        //从在线用户缓存中移除
        await _cache.SetAsync(cacheKey, onlineUsers);

        onlineUsers = onlineUsers?.Where(x => x.TenantId == currentUser.TenantId).ToList();

        string groupName = NoticeConsts.TenantGroupName(currentUser.TenantId);
        //从租户组中移除当前链接用户
        await _hub.Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //向当前组发送离线通知
        await _hub.Clients.Groups(groupName).OnlineUserList(new OnlineUserChangeNotice
        {
            RealName = currentUser.RealName,
            Online = false,
            NoticeUsers = onlineUsers
        });
    }
}
