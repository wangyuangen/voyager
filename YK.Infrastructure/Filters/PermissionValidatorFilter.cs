using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YK.Core.Attributes;

namespace YK.Infrastructure.Filters;

/// <summary>
/// 权限验证过滤器
/// </summary>
public class PermissionValidatorFilter(IPermissionValidator _permissionValidator) : IAuthorizationFilter, IAsyncAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ValidatorAsync(context).Wait();
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        return ValidatorAsync(context);
    }

    private async Task ValidatorAsync(AuthorizationFilterContext context)
    {
        //是否允许匿名访问
        bool allowAnonymous = IsAllow<AllowAnonymousAttribute>(context);

        //是否允许所有用户访问
        bool allowEveryUser = IsAllow<AllowEveryUserAttribute>(context);

        //判断是否有访问权限
        var hasPermission = await _permissionValidator.ValidateAsync();

        if (!allowAnonymous && !allowEveryUser && !hasPermission)
        {
            context.Result = new ForbidResult();
            return;
        }
    }

    /// <summary>
    /// 是否允许访问
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private bool IsAllow<T>(AuthorizationFilterContext context)
        where T:Attribute =>
        context.ActionDescriptor.EndpointMetadata.Any(x => x.GetType() == typeof(T)) is true;
}
