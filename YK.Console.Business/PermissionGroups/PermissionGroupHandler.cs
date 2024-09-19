using System.Linq.Expressions;
using YK.Console.Business.PackagePermissionGroups;
using YK.Console.Business.PermissionGroupApis;
using YK.Console.Business.TenantPackages;

namespace YK.Console.Business.PermissionGroups;

internal class CreatePermissionGroupHandler(IRepository<PermissionGroup> _repo,ISender _sender) : IRequestHandler<CreatePermissionGroupRequest, Guid>
{
    public async Task<Guid> Handle(CreatePermissionGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<PermissionGroup>();
        await _repo.AddAsync(entity,cancellationToken);
        //保存权限组接口
        await _sender.Send(new SavePermissionGroupApiRequest
        {
            ApiIds = request.ApiIds,
            PermissionGroupId = entity.Id,
        }, cancellationToken);
        return entity.Id;
    }
}

internal class UpdatePermissionGroupHandler(IRepository<PermissionGroup> _repo, ISender _sender) : IRequestHandler<UpdatePermissionGroupRequest, Guid>
{
    public async Task<Guid> Handle(UpdatePermissionGroupRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw ResultOutput.Exception("权限组不存在");
        entity.Update(request.Name, request.Remark, request.Scope, request.Sort, request.Enabled, request.ParentId);
        await _repo.UpdateAsync(entity, cancellationToken);
        //保存权限组接口
        await _sender.Send(new SavePermissionGroupApiRequest
        {
            PermissionGroupId = entity.Id,
            ApiIds = request.ApiIds
        }, cancellationToken);

        return entity.Id;
    }
}

internal class DeletePermissionGroupHandler(IRepository<PermissionGroup> _repo) : IRequestHandler<DeletePermissionGroupRequest, int>
{
    public Task<int> Handle(DeletePermissionGroupRequest request, CancellationToken cancellationToken)
        => _repo.SoftDeleteAsync(x => x.Id == request.Id, cancellationToken);
}


internal class PermissionGroupSearchHandler(IReadRepository<PermissionGroup> _repo) : IRequestHandler<PermissionGroupSearchRequest, List<PermissionGroupOutput>>
{
    public Task<List<PermissionGroupOutput>> Handle(PermissionGroupSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<PermissionGroup, bool>>? expression = request.Enabled.HasValue
           ? x => x.Enabled == request.Enabled
           : null;
        return _repo.SimpleListAsync<PermissionGroupOutput>(request, expression, cancellationToken);
    }
}

internal class PermissionGroupWithApiSearchHandler(IReadRepository<PermissionGroup> _repo) : IRequestHandler<PermissionGroupWithApiSearchRequest, List<PermissionGroupWithApiOutput>>
{
    public Task<List<PermissionGroupWithApiOutput>> Handle(PermissionGroupWithApiSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<PermissionGroup, bool>>? expression = request.Enabled.HasValue
          ? x => x.Enabled == request.Enabled
          : null;
        return _repo.SimpleListAsync<PermissionGroupWithApiOutput>(request, expression, cancellationToken);
    }
}

internal class SearchAuthPermissionGroupHandler(ICurrentUser _currentUser, ISender _sender) : IRequestHandler<SearchAuthPermissionGroupRequest, List<PermissionGroupOutput>>
{
    public async Task<List<PermissionGroupOutput>> Handle(SearchAuthPermissionGroupRequest request, CancellationToken cancellationToken)
    {
        if (_currentUser.TenantType == TenantTypeEnum.NormalTenant)
        {
            //普通租户从套餐获取
            var tenantPackages = await _sender.Send(new TenantPackageSearchByTenantRequest
            {
                TenantId = _currentUser.TenantId ?? Guid.Empty
            }, cancellationToken);

            var packageIds = tenantPackages.Select(x => x.PackageId).Distinct().ToList();

            var packagePermissionGroups = await _sender.Send(new PkgPermissionGroupSearchByPkgIdsRequest
            {
                PackageIds = packageIds
            }, cancellationToken);

            return packagePermissionGroups.Select(x => x.PermissionGroup).Distinct().ToList();
        }

        return await _sender.Send(new PermissionGroupSearchRequest
        {
            Enabled = EnabledStatusEnum.Enabled
        }, cancellationToken);
    }
}