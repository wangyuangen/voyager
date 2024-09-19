using Microsoft.AspNetCore.Mvc;
using YK.Console.Business.FileStorageInfos;
using YK.Console.Core.Consts;
using YK.Core.Attributes;

namespace YK.Console;

/// <summary>
/// 文件存储服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup),Order(115)]
public class FileStorageService(ISender _sender):IDynamicApi
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<FileStorageInfoSimpleOutput> UploadAsync([FromForm]UploadFileStorageInfoRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> DeleteAsync([Required] Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeleteFileStorageInfoRequest(id),cancellationToken);

    /// <summary>
    /// 文件绑定业务实体
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<Guid> BindAsync(FileStorageInfoBindBizRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 查询业务实体文件列表
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<List<FileStorageInfoSimpleOutput>> BizFilesAsync(FileStorageInfoSearchByBizRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 获取单个文件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<FileStorageInfoSimpleOutput> GetAsync([Required] Guid id, CancellationToken cancellationToken)
        => _sender.Send(new GetFileStorageInfoRequest(id), cancellationToken);

    /// <summary>
    /// 文件分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<FileStorageInfoOutput>> PageAsync(FileStorageInfoPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);
}
