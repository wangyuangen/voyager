using Microsoft.AspNetCore.Http;
using YK.Core.Contract;
using YK.Module.Core.Contracts;
using YK.Module.Core.Models;

namespace YK.Module.Core.Abstractions;

/// <summary>
/// 控制中心公共服务
/// </summary>
public interface IConsoleService:IScopedService
{
    /// <summary>
    /// 获取员工详情
    /// </summary>
    /// <param name="userStaffId">员工Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserStaffOutput?> GetUserStaffAsync(Guid userStaffId, CancellationToken cancellationToken);

    /// <summary>
    /// 获取子级机构列表
    /// </summary>
    /// <param name="parentOrgId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<OrganizeInfoOutput>> GetChildOrgsAsync(Guid parentOrgId, CancellationToken cancellationToken);

    /// <summary>
    /// 获取员工列表
    /// </summary>
    /// <param name="userStaffIds">员工id集合</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<UserStaffOutput>> GetUserStaffAsync(List<Guid> userStaffIds, CancellationToken cancellationToken);

    /// <summary>
    /// 获取岗位列表
    /// </summary>
    /// <param name="postIds">岗位Id集合</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<PostInfoOutput>> GetPostsAsync(List<Guid> postIds, CancellationToken cancellationToken);

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="bizInfo"></param>
    /// <param name="reName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FileStorageInfoSimpleOutput> FileUploadAsync(IFormFile file, BizInfoOutput bizInfo, bool reName = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取文件列表
    /// </summary>
    /// <param name="bizInfo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<FileStorageInfoSimpleOutput>> GetFilesByBizAsync(BizInfoOutput bizInfo, CancellationToken cancellationToken);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="fileStorageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Guid> DeleteFileAsync(Guid fileStorageId, CancellationToken cancellationToken);

    /// <summary>
    /// 获取文件列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bizIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<FileStorageInfoSimpleOutput>> GetFilesByBizAsync<T>(List<Guid> bizIds, CancellationToken cancellationToken);
}
