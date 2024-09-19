using YK.Console.Business.OrganizeInfos;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 组织机构服务
/// </summary>
[DynamicApi(Area =ConsoleAppConsts.ApiGroup),Order(75)]
public class OrganizeService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 创建组织机构
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateOrganizeInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑组织机构
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateOrganizeInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 删除组织机构
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required]Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeleteOrganizeInfoRequest(id), cancellationToken);

    /// <summary>
    /// 组织机构列表查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<OrganizeInfoOutput>> ListAsync(OrganizeInfoSearchRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);
}
