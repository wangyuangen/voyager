using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YK.Console.Business.Abstractors;
using YK.Core.Events;
using YK.ORM.Specification;

namespace YK.Console.Business.Roles;

internal class GetRoleAuthInfoHandler(IReadRepository<RoleMenuRoute> _menuRepo, IReadRepository<RolePermissionGroup> _permissionRepo)
    : IRequestHandler<GetRoleAuthInfoRequest, RoleAuthInfoOutput>
{
    public async Task<RoleAuthInfoOutput> Handle(GetRoleAuthInfoRequest request, CancellationToken cancellationToken)
    {
        var menuRoutes = await _menuRepo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.RoleId == request.Id)
            .Select(x => x.MenuRouteId)
            .ToListAsync(cancellationToken);

        var permissionGroups = await _permissionRepo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.RoleId == request.Id)
            .Select(x => x.PermissionGroupId)
            .ToListAsync(cancellationToken);

        return new RoleAuthInfoOutput
        {
            MenuRouteIds = menuRoutes,
            PermissionGroupIds = permissionGroups,
            Id = request.Id
        };
    }
}

internal class CreateRoleInfoHandler(IRepository<RoleInfo> _repo) : IRequestHandler<CreateRoleInfoRequest, Guid>
{
    public async Task<Guid> Handle(CreateRoleInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<RoleInfo>();
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdateRoleInfoHandler(IRepository<RoleInfo> _repo) : IRequestHandler<UpdateRoleInfoRequest, Guid>
{
    public async Task<Guid> Handle(UpdateRoleInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        _ = entity ?? throw ResultOutput.Exception("角色不存在");
        entity.Update(request.Name, request.Remark, request.Sort, request.Enabled);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}


internal class DeleteRoleInfoHandler(IRepository<RoleInfo> _repo,IEventPublisher _domainEvent) : IRequestHandler<DeleteRoleInfoRequest, int>
{
    public Task<int> Handle(DeleteRoleInfoRequest request, CancellationToken cancellationToken)
    {
        _domainEvent.PublishAsync(new RoleAuthChangeEvent(request.Id));
        return _repo.SoftDeleteAsync(x => x.Id == request.Id, cancellationToken);
    }
}


internal class RoleInfoPageHandler(IReadRepository<RoleInfo> _repo) : IRequestHandler<RoleInfoPageRequest, PaginationResponse<RoleInfoOutput>>
{
    public Task<PaginationResponse<RoleInfoOutput>> Handle(RoleInfoPageRequest request, CancellationToken cancellationToken)
        => _repo.SimplePageAsync<RoleInfoOutput>(request, cancellationToken: cancellationToken);
}

internal class RoleInfoSearchHandler(IReadRepository<RoleInfo> _repo) : IRequestHandler<RoleInfoSearchRequest, List<RoleInfoOutput>>
{
    public Task<List<RoleInfoOutput>> Handle(RoleInfoSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<RoleInfo, bool>>? expression = request.Enabled.HasValue
           ? x => x.Enabled == request.Enabled
           : null;
        var spec = new EntitiesBaseFilterSortSpec<RoleInfo, RoleInfoOutput>(request, expression, new string[] { nameof(RoleInfo.Sort) });
        return _repo.ListAsync(spec, cancellationToken);
    }
}

internal class RoleInfoAuthChangeHandler(IRoleInfoOperation _operation) : IRequestHandler<RoleInfoAuthChangeRequest, Guid>
{
    public Task<Guid> Handle(RoleInfoAuthChangeRequest request, CancellationToken cancellationToken)
        =>_operation.AuthChangeAsync(request, cancellationToken);
}
