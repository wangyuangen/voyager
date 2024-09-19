using Microsoft.AspNetCore.SignalR;
using YK.Cache.Abstractions;
using YK.Cache.Consts;
using YK.Console.Business.Abstractions;
using YK.Notice;
using YK.Notice.Abstractions;
using YK.Notice.Consts;
using YK.Notice.Models;

namespace YK.Console.Business.OnlineUsers;

public class OnlineUser(ICacheManager _cache, IHubContext<NotificationHub, INotificationHub> _hub,ICurrentUser _currentUser) : IOnlineUser
{
    public async Task ForceOfflineAsync(string connectionId,CancellationToken cancellationToken=default)
    {
        var cachekey = CacheConsts.GetKey<OnlineUserInfo>();
        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cachekey, cancellationToken);

        if (!onlineUsers.IsNullOrEmpty())
        {
            var onlineUser = onlineUsers?.FirstOrDefault(x => x.ConnectionId == connectionId);
            if (onlineUser != null)
            {
                //从在线用户缓存中移除
                onlineUsers?.Remove(onlineUser);
                await _cache.SetAsync(cachekey, onlineUsers, cancellationToken: cancellationToken);
                //从当前组中移除
                string groupName = NoticeConsts.TenantGroupName(onlineUser.TenantId);
                await _hub.Groups.RemoveFromGroupAsync(connectionId, groupName, cancellationToken);
            }
        }
        //发送下线消息
        await _hub.Clients.Client(connectionId).ForceOffline(NoticeConsts.ForceOfflineContent);
    }

    public async Task<PaginationResponse<OnlineUserInfo>> PageAsync(PaginationFilter filter, CancellationToken cancellationToken = default)
    {
        var cachekey = CacheConsts.GetKey<OnlineUserInfo>();
        var onlineUsers = await _cache.GetAsync<List<OnlineUserInfo>>(cachekey, cancellationToken);

        PaginationResponse<OnlineUserInfo> result;

        if (onlineUsers.IsNullOrEmpty())
        {
            result = new PaginationResponse<OnlineUserInfo>(onlineUsers, 0, filter.PageNumber, filter.PageSize);
            return result;
        }

        if (_currentUser.TenantType != TenantTypeEnum.PlatformTenant)
        {
            //非平台租户查看当前租户的在线用户
            onlineUsers = onlineUsers?.Where(x => x.TenantId == _currentUser.TenantId).ToList();
        }

        if(!filter.Keyword.IsNullOrEmpty())
        {
            string keyword = filter.Keyword ?? string.Empty;
            onlineUsers = onlineUsers?.Where(x => x.NickName.Contains(keyword) || x.RealName.Contains(keyword)).ToList();
        }

        int totalCount = onlineUsers?.Count ?? 0;
        var list = onlineUsers?.Skip(filter.PageSize * (filter.PageNumber - 1)).Take(filter.PageSize).ToList();

        result = new PaginationResponse<OnlineUserInfo>(list, totalCount, filter.PageNumber, filter.PageSize);

        return result;
    }
}
