using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using OnceMi.AspNetCore.OSS;
using OSSOptions = YK.Core.Options.OSSOptions;

namespace YK.Infrastructure.FileStorage;

internal static class Extensions
{
    /// <summary>
    /// 注入文件存储服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    internal static IServiceCollection AddStorage(this IServiceCollection services,IConfiguration configuration)
    {
        var options = configuration.GetOptions<FIleUploadOptions>();
        if (options.EnabledOssStorage)
        {
            var ossOptions = options.OSSOptions.FirstOrDefault(x => x.Provider == options.OSSProvider);
            if (ossOptions != null)
            {
                services.AddOSSService(ossOptions.Provider.ToString(), opt =>
                {
                    opt.Provider = ossOptions.Provider;
                    opt.Endpoint = ossOptions.Endpoint;
                    opt.Region = ossOptions.Region;
                    opt.AccessKey = ossOptions.AccessKey;
                    opt.SecretKey = ossOptions.SecretKey;
                    opt.IsEnableHttps = ossOptions.IsEnableHttps;
                    opt.IsEnableCache = ossOptions.IsEnableCache;
                });

                services.InitBucket(ossOptions);
            }
        }
        else
        {
            services.AddOSSService(opt =>
            {
                opt.Provider = OSSProvider.Invalid;
            });
        }
        return services;
    }

    internal static IApplicationBuilder UseStorage(this IApplicationBuilder app, IConfiguration configuration)
    {
        var options = configuration.GetOptions<FIleUploadOptions>();

        var staticDirectory = Path.Combine(Directory.GetCurrentDirectory(), options.RootDirectory);
        
        if (!Directory.Exists(staticDirectory)) Directory.CreateDirectory(staticDirectory);
        
        return app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(staticDirectory),
            RequestPath = new PathString($"/{options.RootDirectory}")
        });
    }

    private static void InitBucket(this IServiceCollection services,OSSOptions ossOptions)
    {
        var ossFactory = services.BuildServiceProvider().GetRequiredService<IOSSServiceFactory>();
        if (ossFactory != null)
        {
            var ossService = ossFactory.Create(ossOptions.Provider.ToString());
            if (ossService != null)
            {
                if (!ossService.BucketExistsAsync(ossOptions.BucketName).Result)
                {
                    ossService.CreateBucketAsync(ossOptions.BucketName).Wait();
                }
            }
        }
    }
}
