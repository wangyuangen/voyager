using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YK.Core.Contract;
using YK.Core.Extensions;

namespace YK.Core;

public class AppCore
{
    private static readonly object _lockObj = new object();
    
    private static IServiceProvider? _serviceProvider = null;

    public static IServiceCollection ServiceCollection { get; set; }

    public static IServiceProvider ServiceProvider 
    {
        get
        {
            if(_serviceProvider == null)
            {
                lock (_lockObj)
                {
                    if(_serviceProvider == null)
                    {
                        //当所有服务注入后才能调用,否则从当前构建的ServiceProvider中获取Service时会出现找不到的情况
                        //或者改为每次调用ServiceProvider都重新构建
                        _serviceProvider = ServiceCollection.BuildServiceProvider();
                    }
                }
            }
            return _serviceProvider;
        }
     }

    public static IConfiguration Configuration { get; set; }

    public static object? GetService(Type serviceType)
    {
        return ServiceProvider.GetService(serviceType);
    }

    public static T? GetService<T>()
    {
        return ServiceProvider.GetService<T>();
    }

    public static IEnumerable<T> GetServices<T>()
    {
        return ServiceProvider.GetServices<T>();
    }

    public static object GetRequiredService(Type serviceType)
    {
        return ServiceProvider.GetRequiredService(serviceType);
    }

    public static T GetRequiredService<T>()
        where T : notnull
    {
        return ServiceProvider.GetRequiredService<T>();
    }

    public static IEnumerable<object?> GetServices(Type serviceType)
    {
        return ServiceProvider.GetServices(serviceType);
    }

    public static T GetConfig<T>(string key = "")
    {
        return Configuration.GetOptions<T>(key);
    }
}
