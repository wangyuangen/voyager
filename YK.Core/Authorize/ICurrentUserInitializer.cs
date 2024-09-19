using System.Security.Claims;

namespace YK.Core.Authorize;

public interface ICurrentUserInitializer
{
    /// <summary>
    /// 写入当前用户身份信息
    /// </summary>
    /// <param name="user"></param>
    void SetCurrentUser(ClaimsPrincipal user);

    /// <summary>
    /// 写入当前用户Id
    /// </summary>
    /// <param name="userId"></param>
    void SetCurrentUserId(string userId);

    /// <summary>
    /// 写入当前职工所属租户Id
    /// </summary>
    /// <param name="tenantId"></param>
    void SetCurrentTenantId(string tenantId);
}
