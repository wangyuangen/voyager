using Microsoft.AspNetCore.Http;
using Serilog.Context;
using YK.Core.Enums;

namespace YK.Infrastructure.Middlewares;

/// <summary>
/// 异常中间件
/// </summary>
/// <param name="serializer"></param>
/// <param name="currentUser"></param>
public class ExceptionMiddleware(ISerializerService serializer,ICurrentUser currentUser) : IMiddleware
{
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly ISerializerService _serializer = serializer;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            await HandleAppExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleAppExceptionAsync(HttpContext context, AppException appException)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = appException.StatusCode;

        var userId = _currentUser.UserId;
        if (userId != Guid.Empty) LogContext.PushProperty("UserId", userId);
        LogContext.PushProperty("StatusCode", appException.StatusCode);
        Log.Error(appException, appException.Message);

        var result = ResultOutput.NotOk(appException.AppMessage);
        result.Code = appException.AppCode.HasValue
            ? appException.AppCode.Value
            : appException.StatusCode;

        return context.Response.WriteAsync(_serializer.Serialize(result));
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = HttpStatusCode.Status500InternalServerError.Value();

        var authorization = context.Request.Headers.Authorization.FirstOrDefault();
        var userAgent = context.Request.Headers.UserAgent.FirstOrDefault();
        context.Items.TryGetValue("_ActionArguments", out object? actionArguments);

        var userId = _currentUser.UserId;
        if (userId != Guid.Empty) LogContext.PushProperty("UserId", userId);
        
        LogContext.PushProperty("StackTrace", exception.StackTrace);
        Log.Error(exception,
        "Error while processing request. \r\nActionArguments: {ActionArguments} \r\nAuthorization: {Authorization} \r\nUserAgent: {UserAgent}",
        actionArguments != null ? _serializer.Serialize(actionArguments) : "",
        authorization,
        userAgent);

        var result = ResultOutput.NotOk(exception.Message);
        result.Code = HttpStatusCode.Status500InternalServerError.Value();
        return context.Response.WriteAsync(_serializer.Serialize(result));
    }
}
