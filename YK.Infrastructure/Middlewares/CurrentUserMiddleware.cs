using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace YK.Infrastructure.Middlewares;

/// <summary>
/// 当前用户中间件
/// </summary>
public class CurrentUserMiddleware(ICurrentUserInitializer _currentUserInitializer) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        bool isAuthenticated = context.User.Identity?.IsAuthenticated is true;
        bool allowAnonymous = context.GetEndpoint()?.Metadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute)) is true;

        if (!allowAnonymous && !isAuthenticated)
        {
            //认证失败
            await context.ChallengeAsync();
            return;
        }

        if (isAuthenticated)
        {
            //认证成功记录当前用户
            _currentUserInitializer.SetCurrentUser(context.User);
        }
       
        await next(context);
    }
}
