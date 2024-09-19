using YK.Console.Business.UserStaffInfos;
using YK.Console.Core.Consts;
using YK.Console.Core.Entities;
using YK.Core.Attributes;
using YK.Core.Authorize;
using YK.Module.Core.Models;
using YK.ORM.Abstractions;

namespace YK.Console;

/// <summary>
/// 员工服务
/// </summary>
[DynamicApi(Area =ConsoleAppConsts.ApiGroup),Order(90)]
public class UserStaffService(ISender _sender,ICurrentUser _currentUser,IReadRepository<UserStaffInfo> _repo):IDynamicApi
{
    /// <summary>
    /// 创建员工
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateUserStaffRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑员工
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Guid> UpdateAsync(UpdateUserStaffRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 删除员工
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> DeleteAsync([Required] Guid id, CancellationToken cancellationToken)
        => _sender.Send(new DeleteUserStaffRequest(id), cancellationToken);

    /// <summary>
    /// 员工分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<UserStaffPageOutput>> PageAsync(UserStaffPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 员工表单扩展信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserStaffExtendOutput> ExtendFormAsync([Required] Guid id, CancellationToken cancellationToken)
        => _sender.Send(new GetUserStaffExtendRequest(id), cancellationToken);

    /// <summary>
    /// 获取当前用户的多重员工身份
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<List<UserStaffOutput>> CurrentUserStaffAsync(CancellationToken cancellationToken)
        => _sender.Send(new GetCurrentUserStaffRequest(), cancellationToken);

    /// <summary>
    /// 获取当前个人身份信息
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public async Task<UserStaffPageOutput> GetPersonalAsync(CancellationToken cancellationToken)
        => await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true)
            .SimpleSingleAsync<UserStaffPageOutput>(x => x.Id == _currentUser.UserStaffId, cancellationToken)
            ?? throw ResultOutput.Exception("当前身份信息不存在");
}
