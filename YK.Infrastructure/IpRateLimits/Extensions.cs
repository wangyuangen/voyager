using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using YK.Cache.Options;

namespace YK.Infrastructure.IpRateLimits;

internal static class Extensions
{
    /// <summary>
    /// 添加Ip限流
    /// </summary>
    /// <param name="serviceBuider"></param>
    /// <returns></returns>
    internal static ICoreServiceBuider AddIpRateLimit(this ICoreServiceBuider serviceBuider)
    {
        var cacheOptions = AppCore.GetConfig<CacheOptions>();

        if (cacheOptions.UseDistributedCache)
        {
            serviceBuider.Services.AddDistributedRateLimiting();
        }
        else
        {
            serviceBuider.Services.AddInMemoryRateLimiting();
        }
        serviceBuider.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        serviceBuider.AppBuilder += app => app.UseIpRateLimiting();

        return serviceBuider;
    }
}
