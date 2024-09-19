using System.Linq.Expressions;
using YK.Console.Business.Abstractors;

namespace YK.Console.Business.ApiInfos;

internal class ApiInfoEnabledStatusChangeHandler(IRepository<ApiInfo> _repo) : IRequestHandler<ApiInfoEnabledStatusChangeRequest, Guid>
{
    public async Task<Guid> Handle(ApiInfoEnabledStatusChangeRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        _ = entity ?? throw ResultOutput.Exception("接口不存在");
        entity.Enabled = request.Enabled;
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class SyncApiFromSwaggerHandler(IApiInfoOperation _apiSync) : IRequestHandler<SyncApiFromSwaggerRequest, bool>
{
    public Task<bool> Handle(SyncApiFromSwaggerRequest request, CancellationToken cancellationToken)
        => _apiSync.SyncAsync(request, cancellationToken);
}


internal class ApiInfoSearchHandler(IReadRepository<ApiInfo> _repo) : IRequestHandler<ApiInfoSearchRequest, List<ApiInfoOutput>>
{
    public Task<List<ApiInfoOutput>> Handle(ApiInfoSearchRequest request, CancellationToken cancellationToken)
    {
        Expression<Func<ApiInfo, bool>>? expression = request.Enabled.HasValue
            ? x => x.Enabled == request.Enabled
            : null;
        return  _repo.SimpleListAsync<ApiInfoOutput>(request, expression, cancellationToken);
    }
}
