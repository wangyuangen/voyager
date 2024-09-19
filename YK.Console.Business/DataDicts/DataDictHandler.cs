using System.Linq.Expressions;
namespace YK.Console.Business.DataDicts;

internal class CreateDataDictHandler(IRepository<DataDictInfo> _repo) : IRequestHandler<CreateDataDictRequest, Guid>
{
    public async Task<Guid> Handle(CreateDataDictRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<DataDictInfo>();
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdateDataDictHandler(IRepository<DataDictInfo> _repo) : IRequestHandler<UpdateDataDictRequest, Guid>
{
    public async Task<Guid> Handle(UpdateDataDictRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw ResultOutput.Exception("字典不存在");
        entity.Update(request.ParentCode, request.Code, request.Name, request.Sort, request.ThemeStyle, request.Remark, request.Enabled);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class DeleteDataDictHandler (IRepository<DataDictInfo> _repo): IRequestHandler<DeleteDataDictRequest, int>
{
    public Task<int> Handle(DeleteDataDictRequest request, CancellationToken cancellationToken)
        => _repo.DeleteAsync(x => x.Id == request.Id, cancellationToken);
}

internal class DataDictSearchHandler(IReadRepository<DataDictInfo> _repo)  : IRequestHandler<DataDictSearchRequest, List<DataDictOutput>>
{
    public Task<List<DataDictOutput>> Handle(DataDictSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<DataDictInfo, bool>>? expression = request.Enabled.HasValue
         ? x => x.Enabled == request.Enabled
         : null;

        return _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true).SimpleListAsync<DataDictOutput>(expression, cancellationToken);
    }
}

internal class DataDictPageHandler(IReadRepository<DataDictInfo> _repo) : IRequestHandler<DataDictPageRequest, PaginationResponse<DataDictOutput>>
{
    public Task<PaginationResponse<DataDictOutput>> Handle(DataDictPageRequest request, CancellationToken cancellationToken)
       => _repo.SimplePageAsync<DataDictOutput>(request, x => x.ParentCode == request.ParentCode, cancellationToken: cancellationToken);
}
