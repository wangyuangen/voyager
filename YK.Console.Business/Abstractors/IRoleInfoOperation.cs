using YK.Console.Business.Roles;

namespace YK.Console.Business.Abstractors;

/// <summary>
/// 角色
/// </summary>
[UnitOfWork]
public interface IRoleInfoOperation: IScopedService
{
    /// <summary>
    /// 角色授权变更
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UnitOfWork]
    Task<Guid> AuthChangeAsync(RoleInfoAuthChangeRequest request, CancellationToken cancellationToken);
}
