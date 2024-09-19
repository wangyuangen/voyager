using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace YK.Infrastructure.Middlewares;

internal static class Extensions
{
    /// <summary>
    /// 注入中间件
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    internal static IServiceCollection AddCustomMiddleware(this IServiceCollection services, AppOptions options)
    {
        services.AddScoped<CurrentUserMiddleware>();
        services.AddScoped<ExceptionMiddleware>();

        if (options.EnableHttpLogging)
        {
            services.AddSingleton<RequestLoggingMiddleware>();
            services.AddScoped<ResponseLoggingMiddleware>();
        }

        return services;
    }

    /// <summary>
    /// 配置中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    internal static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app, AppOptions options)
    {
        app.UseMiddleware<CurrentUserMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();

        if (options.EnableHttpLogging)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ResponseLoggingMiddleware>();
        }

        return app;
    }
}
