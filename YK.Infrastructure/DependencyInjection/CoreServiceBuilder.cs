using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YK.Infrastructure.DependencyInjection;

public class CoreServiceBuilder : ICoreServiceBuider
{
    public IServiceCollection Services { get; }

    public Action<IApplicationBuilder> AppBuilder { get; set; }

    public CoreServiceBuilder(IServiceCollection services)
    {
        Services = services;
        AppCore.ServiceCollection = Services;
        AppCore.Configuration = services
            .BuildServiceProvider()
            .GetService<IConfiguration>() ?? throw new ArgumentNullException("configuration service not found");
    }
}
