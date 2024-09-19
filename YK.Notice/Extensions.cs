using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using YK.Core;
using YK.Core.DependencyInjection;
using YK.Notice.Abstractions;
using YK.Notice.Options;
namespace YK.Notice;

public static class Extensions
{
    public static ICoreServiceBuider AddNotifications(this ICoreServiceBuider coreServiceBuider)
    {
        ILogger logger = Log.ForContext(typeof(Extensions));

        var signalROptions = AppCore.GetConfig<SignalROptions>();

        if (!signalROptions.UseBackplane)
        {
            coreServiceBuider.Services.AddSignalR(opt =>
            {
                opt.EnableDetailedErrors = true;
                opt.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
                opt.KeepAliveInterval = TimeSpan.FromMinutes(1);
                opt.MaximumReceiveMessageSize = 1024 * 1024 * 10; // 数据包大小10M，默认最大为32K
            }).AddNewtonsoftJsonProtocol(opt =>
            {
                opt.PayloadSerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                opt.PayloadSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                opt.PayloadSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; // 时间格式化
                opt.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // 忽略循环引用
            });
        }
        else
        {
            var backplaneSettings = signalROptions.Backplane;
            if (backplaneSettings is null) throw new InvalidOperationException("Backplane enabled, but no backplane settings in config.");
            switch (backplaneSettings.Provider)
            {
                case "redis":
                    if (backplaneSettings.StringConnection is null) throw new InvalidOperationException("Redis backplane provider: No connectionString configured.");
                    coreServiceBuider.Services.AddSignalR().AddStackExchangeRedis(backplaneSettings.StringConnection, options =>
                    {
                        options.Configuration.AbortOnConnectFail = false;
                        options.Configuration.Password = backplaneSettings.Auth;
                    });
                    break;

                default:
                    throw new InvalidOperationException($"SignalR backplane Provider {backplaneSettings.Provider} is not supported.");
            }

            logger.Information($"SignalR Backplane Current Provider: {backplaneSettings.Provider}.");
        }

        coreServiceBuider.Services.AddScoped<INoticeMessage, NoticeMessage>();

        return coreServiceBuider;
    }

    public static IEndpointRouteBuilder MapNotifications(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationHub>("/hub/notifications", options =>
        {
            options.CloseOnAuthenticationExpiration = true;
        });
        return endpoints;
    }
}
