using YK.Console.Business.MenuRoutes;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 菜单服务
/// </summary>
[DynamicApi(Area =ConsoleAppConsts.ApiGroup),Order(64)]
public class MenuRouteService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateMenuRouteRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateMenuRouteRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeleteMenuRouteRequest(id), cancellationToken);

    /// <summary>
    /// 菜单列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<MenuRouteOutput>> ListAsync(MenuRouteSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 可被授予的菜单
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<MenuRouteSimpleOutput>> AuthMenuRoutesAsync(CancellationToken cancellationToken)
        => _sender.Send(new SearchAuthMenuRoutesRequest(), cancellationToken);
}
