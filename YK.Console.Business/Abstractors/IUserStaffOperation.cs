using YK.Console.Business.UserStaffInfos;

namespace YK.Console.Business.Abstractors;

/// <summary>
/// 用户员工
/// </summary>
[UnitOfWork]
public interface IUserStaffOperation:IScopedService
{
    /// <summary>
    /// 创建员工
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UnitOfWork]
    Task<Guid> CreateAsync(CreateUserStaffRequest request,CancellationToken cancellationToken);

    /// <summary>
    /// 编辑员工
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UnitOfWork]
    Task<Guid> UpdateAsync(UpdateUserStaffRequest request, CancellationToken cancellationToken);
}
