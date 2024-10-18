using Microsoft.EntityFrameworkCore;
using YK.Cache.Abstractions;
using YK.Cache.Consts;
using YK.Console.Business.PermissionGroupApis;
using YK.Core.Events;

namespace YK.Console.Business.Roles;

internal class RoleAuthChangeEvent:DomainEvent
{
    public Guid RoleId { get; set; }
    public RoleAuthChangeEvent(Guid roleId)
    {
        RoleId = roleId;
    }
}

internal class ClearRoleAuthByRoleAuthChangeHandler(
    ICacheManager _cache,
    ICurrentUser _currentUser,
    IReadRepository<UserStaffRole> _staffRoleRepo,
    ILogger<ClearRoleAuthByRoleAuthChangeHandler> _logger)
    : IEventNotificationHandler<RoleAuthChangeEvent>
{
    public async Task Handle(EventNotification<RoleAuthChangeEvent> notification, CancellationToken cancellationToken)
    {
        var userStaffIds = await _staffRoleRepo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x=>x.RoleId == notification.Event.RoleId)
            .Select(x => x.UserStaffId)
            .ToListAsync(cancellationToken);

        string cacheKey;
        foreach (var staffId in userStaffIds)
        {
            cacheKey = CacheConsts.GetKey<ApiPermissionDataScope>(_currentUser.TenantId, staffId);
            await _cache.RemoveAsync(cacheKey, cancellationToken);
        }

        _logger.LogInformation("{event} Triggered", notification);
    }
}
