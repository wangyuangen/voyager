using YK.Console.Business.ViewInfos;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 视图服务
/// </summary>
[DynamicApi(Area =ConsoleAppConsts.ApiGroup),Order(70)]
public class ViewService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 创建视图
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateViewInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑视图
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateViewInfoRequest request,CancellationToken cancellationToken)
        =>_sender.Send(request, cancellationToken);

    /// <summary>
    /// 删除视图
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeleteViewInfoRequest(id), cancellationToken);

    /// <summary>
    /// 视图列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<ViewInfoOutput>> ListAsync(ViewInfoSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);
}
