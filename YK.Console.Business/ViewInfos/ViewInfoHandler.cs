using System.Linq.Expressions;
using YK.ORM.Specification;

namespace YK.Console.Business.ViewInfos;

internal class CreateViewInfoHandler(IRepository<ViewInfo> _repo) : IRequestHandler<CreateViewInfoRequest, Guid>
{
    public async Task<Guid> Handle(CreateViewInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<ViewInfo>();
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdateViewInfoHandler(IRepository<ViewInfo> _repo) : IRequestHandler<UpdateViewInfoRequest, Guid>
{
    public async Task<Guid> Handle(UpdateViewInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id,cancellationToken);
        _ = entity ?? throw ResultOutput.Exception("视图不存在");
        entity.Update(request.RouteName,request.Name,request.Path,request.Remark,request.IsKeepAlive,request.Sort,request.Enabled,request.ParentId);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class DeleteViewInfoHandler(IRepository<ViewInfo> _repo) : IRequestHandler<DeleteViewInfoRequest, int>
{
    public Task<int> Handle(DeleteViewInfoRequest request, CancellationToken cancellationToken)
        => _repo.SoftDeleteAsync(x => x.Id == request.Id, cancellationToken);
}


internal class ViewInfoSearchHandler(IReadRepository<ViewInfo> _repo) : IRequestHandler<ViewInfoSearchRequest, List<ViewInfoOutput>>
{
    public Task<List<ViewInfoOutput>> Handle(ViewInfoSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<ViewInfo, bool>>? expression = request.Enabled.HasValue
           ? x => x.Enabled == request.Enabled
           : null;
        var spec = new EntitiesBaseFilterSortSpec<ViewInfo, ViewInfoOutput>(request, expression, new string[] { nameof(ViewInfo.Sort) });
        return _repo.ListAsync(spec, cancellationToken);
    }
}
