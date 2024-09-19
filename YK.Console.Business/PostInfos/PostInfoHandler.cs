namespace YK.Console.Business.PostInfos;

internal class CreatePostInfoHandler(IRepository<PostInfo> _repo) : IRequestHandler<CreatePostInfoRequest, Guid>
{
    public async Task<Guid> Handle(CreatePostInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<PostInfo>();
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class UpdatePostInfoHandler(IRepository<PostInfo> _repo) : IRequestHandler<UpdatePostInfoRequest, Guid>
{
    public async Task<Guid> Handle(UpdatePostInfoRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
        _ = entity ?? throw ResultOutput.Exception("岗位不存在");
        entity.Update(request.Name, request.Remark);
        await _repo.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }
}

internal class DeletePostInfoHandler(IRepository<PostInfo> _repo) : IRequestHandler<DeletePostInfoRequest, int>
{
    public Task<int> Handle(DeletePostInfoRequest request, CancellationToken cancellationToken)
        => _repo.DeleteAsync(x => x.Id == request.Id, cancellationToken);
}


internal class PostInfoSearchHandler(IReadRepository<PostInfo> _repo) : IRequestHandler<PostInfoSearchRequest, List<PostInfoOutput>>
{
    public Task<List<PostInfoOutput>> Handle(PostInfoSearchRequest request, CancellationToken cancellationToken)
        => _repo.SimpleListAsync<PostInfoOutput>(request, cancellationToken: cancellationToken);
}


internal class PostInfoPageHandler(IReadRepository<PostInfo> _repo) : IRequestHandler<PostInfoPageSearchRequest, PaginationResponse<PostInfoOutput>>
{
    public Task<PaginationResponse<PostInfoOutput>> Handle(PostInfoPageSearchRequest request, CancellationToken cancellationToken)
        => _repo.SimplePageAsync<PostInfoOutput>(request, cancellationToken: cancellationToken);
}
