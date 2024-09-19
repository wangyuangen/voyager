using YK.Core.Contract;
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
    Task<UserStaffOutput> GetUserStaffAsync(Guid userStaffId, CancellationToken cancellationToken);

    /// <summary>
    /// 获取岗位列表
    /// </summary>
    /// <param name="postIds">岗位Id集合</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<PostInfoOutput>> GetPostsAsync(List<Guid> postIds, CancellationToken cancellationToken);
}
