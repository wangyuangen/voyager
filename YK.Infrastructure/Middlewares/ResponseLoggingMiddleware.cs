using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace YK.Infrastructure.Middlewares;

/// <summary>
/// http返回日志中间件
/// </summary>
/// <param name="currentUser"></param>
internal class ResponseLoggingMiddleware(ICurrentUser currentUser) : IMiddleware
{
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);
        var originalBody = context.Response.Body;
        using var newBody = new MemoryStream();
        context.Response.Body = newBody;
        string responseBody;
        if (context.Request.Path.ToString().Contains("tokens"))
        {
            responseBody = "[Redacted] Contains Sensitive Information.";
        }
        else
        {
            newBody.Seek(0, SeekOrigin.Begin);
            responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        }

        var userId = _currentUser.UserId;
        if (userId != Guid.Empty) LogContext.PushProperty("UserId", userId);

        var userAccount = _currentUser.UserAccount;

        LogContext.PushProperty("StatusCode", context.Response.StatusCode);
        LogContext.PushProperty("ResponseTime", DateTime.Now);
        Log.ForContext("ResponseHeaders", context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
        .ForContext("ResponseBody", responseBody)
       .Information("HTTP {RequestMethod} Response from {RequestPath} by {UserAccount} has Status Code {StatusCode}.", context.Request.Method, context.Request.Path, string.IsNullOrEmpty(userAccount) ? "Anonymous" : userAccount, context.Response.StatusCode);
        newBody.Seek(0, SeekOrigin.Begin);
        await newBody.CopyToAsync(originalBody);
    }
}
