using YK.Console.Business.DataDicts;
using YK.Console.Core.Consts;
using YK.Core.Attributes;

namespace YK.Console;

/// <summary>
/// 数据字典服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup),Order(110)]
public class DataDictService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 创建字典
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateDataDictRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑字典
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateDataDictRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 删除字典
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeleteDataDictRequest(id), cancellationToken);

    /// <summary>
    /// 字典列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<List<DataDictOutput>> ListAsync(DataDictSearchRequest request,CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 字典分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<DataDictOutput>> PageAsync(DataDictPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);
}
