using YK.Core.Commons.Models;
using YK.Core.Contract;

namespace YK.Core.Authorize;

public interface IPermissionValidator:IScopedService
{
    /// <summary>
    /// 校验接口访问权限
    /// </summary>
    /// <returns></returns>
    Task<bool> ValidateAsync();

    /// <summary>
    /// 获取员工对当前接口的数权范围
    /// </summary>
    /// <returns></returns>
    Task<UserStaffPermissionScope> GetPermissionScopeAsync();
}
