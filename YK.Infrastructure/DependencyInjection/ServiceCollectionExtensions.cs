using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using YK.Infrastructure.Authorize;
using YK.Infrastructure.Commons;
using YK.Infrastructure.Cors;
using YK.Infrastructure.FileStorage;
using YK.Infrastructure.Middlewares;
using YK.ORM.Persistence;

namespace YK.Infrastructure.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    private static CoreServiceBuilder _builder;

    internal static ICoreServiceBuider AddCore(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        var options = configuration.GetOptions<AppOptions>();
        
        var assemblies = Reflections.GetAssemblyList(options.Modules);

        builder.ConfigureSerilog();

        builder.Services.AddHttpContextAccessor()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>())
            .AddTransient<IStartupFilter, StartupFilter>()
            .AddValidatorsFromAssemblies(assemblies,includeInternalTypes:true)
            .AddCustomMiddleware(options)
            .AddServices(options)
            .AddCorsPolicy(configuration)
            .AddStorage(configuration)
            .AddHealthChecks();

        _builder = new CoreServiceBuilder(builder.Services)
        {
            AppBuilder = app =>
            {
                app.UseCorsPolicy()
                   .UseStorage(configuration)
                   .UseRouting();
            }
        };

        return _builder;
    }

    internal class StartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return x =>
            {
                _builder.AppBuilder(x);
                next(x);
                x.InitializerDatabases();
            };
        }
    }
}
