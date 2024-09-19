using Microsoft.Extensions.DependencyInjection;
using YK.Cache.Abstractions;
using YK.Cache.Options;
using YK.Core;
using YK.Core.DependencyInjection;

namespace YK.Cache;

public static class Extensions
{
    /// <summary>
    /// 注入缓存组件
    /// </summary>
    /// <param name="serviceBuider"></param>
    /// <returns></returns>
    public static ICoreServiceBuider AddCaching(this ICoreServiceBuider serviceBuider)
    {
        var options = AppCore.GetConfig<CacheOptions>();

        if (options.UseDistributedCache)
        {
            if (options.PreferRedis)
            {
                serviceBuider.Services.AddStackExchangeRedisCache(opt =>
                {
                    opt.Configuration = options.RedisURL;
                    opt.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
                    {
                        AbortOnConnectFail = true,
                        EndPoints = { options.RedisURL ?? string.Empty },
                        Password = options.RedisAuth
                    };
                });
            }
            else
            {
                serviceBuider.Services.AddDistributedMemoryCache();
            }

            serviceBuider.Services.AddTransient<ICacheManager, DistributedCacheService>();
        }
        else
        {
            serviceBuider.Services.AddMemoryCache();
            serviceBuider.Services.AddTransient<ICacheManager, LocalCacheService>();
        }

        return serviceBuider;
    }
}
