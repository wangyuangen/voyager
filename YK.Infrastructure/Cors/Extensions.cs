using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YK.Infrastructure.Cors;

internal static class Extensions
{
    private const string CorsPolicy = nameof(CorsPolicy);

    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services,IConfiguration config)
    {
        var corsOptions = config.GetOptions<CorsOptions>();
        var origins = new List<string>();

        if (corsOptions.WebWhiteList is not null)
            origins.AddRange(corsOptions.WebWhiteList);

        if (corsOptions.AppWhiteList is not null)
            origins.AddRange(corsOptions.AppWhiteList);

        services.AddCors(opt =>
            opt.AddPolicy(CorsPolicy, policy =>
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(origins.ToArray())));

        return services;
    }

    internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
        app.UseCors(CorsPolicy);
}
