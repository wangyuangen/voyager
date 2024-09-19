using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace YK.Core.DependencyInjection;

public interface ICoreServiceBuider
{
    IServiceCollection Services { get; }

    Action<IApplicationBuilder> AppBuilder { get; set; }
}
