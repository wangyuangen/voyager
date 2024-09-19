using YK.Console.Business.Roles;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 角色服务
/// </summary>
[DynamicApi(Area =ConsoleAppConsts.ApiGroup),Order(95)]
public class RoleService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateRoleInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 编辑角色
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateRoleInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeleteRoleInfoRequest(id), cancellationToken);

    /// <summary>
    /// 角色授权变更
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> AuthChangeAsync(RoleInfoAuthChangeRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 获取角色授权信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<RoleAuthInfoOutput> AuthInfoAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new GetRoleAuthInfoRequest(id), cancellationToken);

    /// <summary>
    /// 角色列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<RoleInfoOutput>> ListAsync(RoleInfoSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 角色分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<RoleInfoOutput>> PageAsync(RoleInfoPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);
}
