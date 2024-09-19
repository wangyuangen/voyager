using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog.Filters;

namespace YK.Infrastructure.Commons;

internal static class Extensions
{
    /// <summary>
    /// 注入服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="appOptions"></param>
    /// <returns></returns>
    internal static IServiceCollection AddServices(this IServiceCollection services,AppOptions appOptions)
    {
        services
            .AddServices(typeof(ITransientService), ServiceLifetime.Transient,appOptions.Modules)
            .AddServices(typeof(ISingletonService), ServiceLifetime.Singleton, appOptions.Modules)
            .AddServices(typeof(IScopedService), ServiceLifetime.Scoped, appOptions.Modules);

        services
               .AddTransient<HttpHandler>()
               .AddHttpClient<IHttpClientService, HttpClientService>()
               .AddHttpMessageHandler<HttpHandler>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime, string[]? modules)
    {
        var interfaceTypes =
                Reflections.GetAssemblyList(modules)
                .SelectMany(s => s.GetTypes())
                .Where(t => interfaceType.IsAssignableFrom(t)
                            && t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null
                            && interfaceType.IsAssignableFrom(t.Service));

        foreach (var type in interfaceTypes)
        {
            services.AddServices(type.Service!, type.Implementation, lifetime);
        }

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
        lifetime switch
        {
            ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
            ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
            ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
            _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
        };

    /// <summary>
    /// Serilog 注入
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    internal static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Host.UseSerilog((context, logger) =>
        {
            logger.ReadFrom.Configuration(context.Configuration);
            logger.Enrich.FromLogContext();
            logger
                .MinimumLevel.Override("Hangfire", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System",LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"));
        });
        return builder;
    }
}
