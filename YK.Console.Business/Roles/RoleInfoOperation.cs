using YK.Console.Business.Abstractors;
using YK.Console.Business.RoleMenuRoutes;
using YK.Console.Business.RolePermissionGroups;
using YK.Core.Events;

namespace YK.Console.Business.Roles;

public class RoleInfoOperation(ISender _sender,IEventPublisher _domainEvent) : IRoleInfoOperation
{
    public async Task<Guid> AuthChangeAsync(RoleInfoAuthChangeRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(new SaveRoleMenuRouteRequest
        {
             RoleId = request.Id,
             MenuRouteIds = request.MenuRouteIds,
        },cancellationToken);

        await _sender.Send(new SaveRolePermissionGroupRequest
        {
             RoleId = request.Id,
             PermissionGroupIds = request.PermissionGroupIds,
        },cancellationToken);

        //角色授权变更后调整相应用户权限
        await _domainEvent.PublishAsync(new RoleAuthChangeEvent(request.Id));

        return request.Id;
    }
}
