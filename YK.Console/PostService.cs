using YK.Console.Business.PostInfos;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 岗位服务
/// </summary>
[DynamicApi(Area =ConsoleAppConsts.ApiGroup),Order(100)]
public class PostService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 创建岗位
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreatePostInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑岗位
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdatePostInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 删除岗位
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeletePostInfoRequest(id), cancellationToken);

    /// <summary>
    /// 岗位列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<PostInfoOutput>> ListAsync(PostInfoSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 岗位分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<PostInfoOutput>> PageAsync(PostInfoPageSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);
}
