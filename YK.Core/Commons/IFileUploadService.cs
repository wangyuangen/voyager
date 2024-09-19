using Microsoft.AspNetCore.Http;
using YK.Core.Commons.Models;
using YK.Core.Contract;

namespace YK.Core.Commons;

/// <summary>
/// 文件上传服务
/// </summary>
public interface IFileUploadService:IScopedService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="reName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FileUploadInfo> UploadAsync(IFormFile file,bool reName=false,CancellationToken cancellationToken=default);

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="dataOfBase64"></param>
    /// <param name="fileName"></param>
    /// <param name="reName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FileUploadInfo> UploadAsync(string dataOfBase64,string fileName, bool reName = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileName"></param>
    /// <param name="reName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FileUploadInfo> UploadAsync(Stream stream,string fileName, bool reName = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(FileUploadInfo file);
}
