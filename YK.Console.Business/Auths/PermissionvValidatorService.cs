using Microsoft.AspNetCore.Http;
using YK.Console.Business.PermissionGroupApis;
using YK.Console.Business.UserStaffOrgs;

namespace YK.Console.Business.Auths;

/// <summary>
/// 接口权限验证
/// </summary>
public class PermissionvValidatorService(ISender _sender,IHttpContextAccessor _httpContext) : IPermissionValidator
{
    public async Task<UserStaffPermissionScope> GetPermissionScopeAsync()
    {
        var currentApiScope = await GetCurrentApiScopeAsync();

        var permissionScope = currentApiScope?.Scope ?? DataPermissionScope.CurrentUserStaff;

        if(permissionScope == DataPermissionScope.All || permissionScope == DataPermissionScope.CurrentUserStaff)
            return new UserStaffPermissionScope(permissionScope);

        var staffPermissionScope = await _sender.Send(new GetUserStaffOrgsByPermissionScopeRequest(permissionScope))
                ?? new UserStaffPermissionScope();

        //#if DEBUG
        //        //DEBUG模式平台管理员默认具有全部权限
        //        var _currentUser = AppCore.GetRequiredService<ICurrentUser>();
        //        if (_currentUser.UserStaffType == UserStaffTypeEnum.PlatformAdmin)
        //            staffPermissionScope.Update(permissionScope: DataPermissionScope.All);
        //#endif


        return staffPermissionScope;
    }

    public async Task<bool> ValidateAsync()
    {

//#if DEBUG
//        var _currentUser = AppCore.GetRequiredService<ICurrentUser>();
//        if (_currentUser.UserStaffType == UserStaffTypeEnum.PlatformAdmin) return true;
//#endif

        var currentApiScope = await GetCurrentApiScopeAsync();
        return !currentApiScope.IsNull();
    }

    /// <summary>
    /// 获取员工当前接口的数权范围
    /// </summary>
    /// <returns></returns>
    private async Task<ApiPermissionDataScope?> GetCurrentApiScopeAsync()
    {
        string apiPath = _httpContext.HttpContext?.Request.Path ?? string.Empty;
        string httpMethod = _httpContext.HttpContext?.Request.Method ?? string.Empty;

        var dataPermissionScopes = await _sender.Send(new CurrentUserApiPermissionDataScopeRequest());

        var result = dataPermissionScopes?.OrderBy(x => x.Scope)
              .FirstOrDefault(x => x.HttpMethod.Equals(httpMethod,StringComparison.OrdinalIgnoreCase) && x.ApiPath == apiPath);

        return result;
    }
}
