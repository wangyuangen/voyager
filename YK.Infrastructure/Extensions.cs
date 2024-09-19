using Microsoft.AspNetCore.Builder;
using YK.Authorize;
using YK.DynamicApi;
using YK.BackgroundJob;
using YK.Infrastructure.DependencyInjection;
using YK.Cache;
using Microsoft.AspNetCore.Routing;
using YK.Notice;
using YK.Infrastructure.Middlewares;
using YK.Infrastructure.IpRateLimits;
using YK.EventBus;

namespace YK.Infrastructure;

/// <summary>
/// 
/// </summary>
public static class Extensions
{
    /// <summary>
    /// 基础设施注入
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static ICoreServiceBuider AddInfrastructure(this WebApplicationBuilder builder)
    {
        return builder.AddCore()
            .AddOpenApi()
            .AddAuthorize()
            .AddHangfireJob()
            .AddCaching()
            .AddNotifications()
            .AddIpRateLimit()
            .AddCapEventBus()
            .RegisterModules();
    }

    public static WebApplication UseInfrastructure(this WebApplication application)
    {
        var appOptions = AppCore.GetConfig<AppOptions>();

        application.UseCustomMiddleware(appOptions);

        application.MapEndpoints();

        return application;
    }

    internal static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers()
            .RequireAuthorization();
        builder.MapHealthCheck();
        builder.MapNotifications();
        return builder;
    }

    private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
            endpoints.MapHealthChecks("/Home/HealthCheck").RequireAuthorization();

    /// <summary>
    /// 注入其他模块
    /// </summary>
    /// <param name="serviceBuider"></param>
    /// <returns></returns>
    private static ICoreServiceBuider RegisterModules(this ICoreServiceBuider serviceBuider)
    {
        var appOptions = AppCore.GetConfig<AppOptions>();

        var modules = Reflections.GetImplementations<IModuleExtension>(appOptions.Modules);

        IModuleExtension? moduleExtension;
        foreach ( var module in modules )
        {
            moduleExtension= (IModuleExtension?)Activator.CreateInstance(module);
            moduleExtension?.Register(serviceBuider);
        }

        return serviceBuider;
    }
}
