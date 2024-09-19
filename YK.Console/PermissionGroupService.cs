using YK.Console.Business.PermissionGroups;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 权限组服务
/// </summary>
/// <param name="_sender"></param>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup),Order(81)]
public class PermissionGroupService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 创建权限组
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreatePermissionGroupRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑权限组
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdatePermissionGroupRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 删除权限组
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeletePermissionGroupRequest(id), cancellationToken);

    /// <summary>
    /// 权限组列表查询(WithApi)
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<PermissionGroupWithApiOutput>> ListAsync(PermissionGroupWithApiSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 可被授予的权限组
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<PermissionGroupOutput>> AuthPermissionGroupAsync(CancellationToken cancellationToken)
        => _sender.Send(new SearchAuthPermissionGroupRequest(), cancellationToken);
}
