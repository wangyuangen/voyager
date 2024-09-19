using System.Linq.Expressions;

namespace YK.Console.Business.OrganizeInfos;

internal class CreateOrganizeInfoHandler(IRepository<OrganizeInfo> _repo) : IRequestHandler<CreateOrganizeInfoRequest, Guid>
{
    public async Task<Guid> Handle(CreateOrganizeInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<OrganizeInfo>();
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdateOrganizeInfoHandler(IRepository<OrganizeInfo> _repo) : IRequestHandler<UpdateOrganizeInfoRequest, Guid>
{
    public async Task<Guid> Handle(UpdateOrganizeInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw ResultOutput.Exception("机构不存在");
        entity.Update(request.ParentId, request.Name, request.OrganizeType, request.Enabled, request.Remark, null, request.Sort,
            request.RegionCode, request.RegionText);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class DeleteOrganizeInfoHandler(IRepository<OrganizeInfo> _repo) : IRequestHandler<DeleteOrganizeInfoRequest, int>
{
    public Task<int> Handle(DeleteOrganizeInfoRequest request, CancellationToken cancellationToken)
        => _repo.DeleteAsync(x => x.Id == request.Id, cancellationToken);
}


internal class OrganizeInfoSearchHandler(IReadRepository<OrganizeInfo> _repo) : IRequestHandler<OrganizeInfoSearchRequest, List<OrganizeInfoOutput>>
{
    public Task<List<OrganizeInfoOutput>> Handle(OrganizeInfoSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<OrganizeInfo, bool>>? expression = x => true;
        if (request.OrganizeType.HasValue)
            expression = expression.AndAlso(x => x.OrganizeType == request.OrganizeType.Value);
        if (request.Enabled.HasValue)
            expression = expression.AndAlso(x => x.Enabled == request.Enabled.Value);

        return _repo.SimpleListAsync<OrganizeInfoOutput>(request, expression, cancellationToken);
    }
}

internal class GetChildOrgsByParentHandler(IReadRepository<OrganizeInfo> _repo) : IRequestHandler<GetChildOrgsByParentRequest, List<OrganizeInfoOutput>>
{
    public async Task<List<OrganizeInfoOutput>> Handle(GetChildOrgsByParentRequest request, CancellationToken cancellationToken)
    {
        var orgs = await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter: true)
             .SimpleListAsync<OrganizeInfoOutput>();

        return GetChilds(orgs, request.ParentId);
    }

    private List<OrganizeInfoOutput> GetChilds(List<OrganizeInfoOutput> all,Guid parentId)
    {
        var result = new List<OrganizeInfoOutput>();

        var childs = all.Where(x => x.ParentId == parentId).ToList();

        if (!childs.IsNullOrEmpty())
        {
            childs.ForEach(child =>
            {
                result.AddRange(GetChilds(all, child.Id));
            });
            result.AddRange(childs);
        }

        return result;
    }
}
