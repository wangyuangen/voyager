using System.Linq.Expressions;
using YK.Console.Business.PackageMenuRoutes;
using YK.Console.Business.TenantPackages;
using YK.ORM.Specification;

namespace YK.Console.Business.MenuRoutes;

internal class CreateMenuRouteHandler(IRepository<MenuRouteInfo> _repo) : IRequestHandler<CreateMenuRouteRequest, Guid>
{
    public async Task<Guid> Handle(CreateMenuRouteRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<MenuRouteInfo>();
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdateMenuRouteHandler(IRepository<MenuRouteInfo> _repo) : IRequestHandler<UpdateMenuRouteRequest, Guid>
{
    public async Task<Guid> Handle(UpdateMenuRouteRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        _ = entity ?? throw ResultOutput.Exception("菜单不存在");
        entity.Update(request.ParentId, request.ViewId, request.Type, request.Name, request.RouteUrl, request.RouteName, request.RedirectUrl,
            request.Icon, request.Hidden, request.Opened, request.NewWindow, request.External, request.IsKeepAlive, request.IsAffix, request.Link,
            request.IsIframe, request.Sort, request.Remark, request.Enabled);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}


internal class DeleteMenuRouteHandler(IRepository<MenuRouteInfo> _repo) : IRequestHandler<DeleteMenuRouteRequest, int>
{
    public Task<int> Handle(DeleteMenuRouteRequest request, CancellationToken cancellationToken)
        => _repo.SoftDeleteAsync(x => x.Id == request.Id, cancellationToken);
}


internal class MenuRouteSearchHandler(IReadRepository<MenuRouteInfo> _repo) : IRequestHandler<MenuRouteSearchRequest, List<MenuRouteOutput>>
{
    public Task<List<MenuRouteOutput>> Handle(MenuRouteSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<MenuRouteInfo, bool>>? expression = request.Enabled.HasValue
           ? x => x.Enabled == request.Enabled
           : null;
        var spec = new EntitiesBaseFilterSortSpec<MenuRouteInfo, MenuRouteOutput>(request, expression, new string[] { nameof(MenuRouteInfo.Sort) });

        return _repo.ListAsync(spec, cancellationToken);
    }
}


internal class SearchAuthMenuRoutesHandler(ICurrentUser _currentUser, ISender _sender,IReadRepository<MenuRouteInfo> _repo) : IRequestHandler<SearchAuthMenuRoutesRequest, List<MenuRouteSimpleOutput>>
{
    public async Task<List<MenuRouteSimpleOutput>> Handle(SearchAuthMenuRoutesRequest request, CancellationToken cancellationToken)
    {
        if (_currentUser.TenantType == TenantTypeEnum.NormalTenant)
        {
            //普通租户从套餐获取
            var tenantPackages = await _sender.Send(new TenantPackageSearchByTenantRequest
            {
                TenantId = _currentUser.TenantId ?? Guid.Empty
            }, cancellationToken);

            var packageIds = tenantPackages.Select(x => x.PackageId).Distinct().ToList();

            var packageMenuRoutes = await _sender.Send(new PackageMenuRouteSearchByPackagesRequest
            {
                PackageIds = packageIds,
            }, cancellationToken);

            return packageMenuRoutes.Select(x => x.MenuRoute).Distinct().OrderBy(x=>x.Sort).ToList();
        }
        var spec = new EntitiesSortExpressionSpec<MenuRouteInfo, MenuRouteSimpleOutput>(x => x.Enabled == EnabledStatusEnum.Enabled, new string[] { nameof(MenuRouteInfo.Sort) });
        return await _repo.ListAsync(spec, cancellationToken);
    }
}