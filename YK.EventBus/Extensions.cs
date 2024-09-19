using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;
using YK.Core;
using YK.Core.Commons;
using YK.Core.DependencyInjection;
using YK.Core.Options;
using YK.EventBus.Abstractions;
using YK.EventBus.Cap;
using YK.EventBus.Options;

namespace YK.EventBus;

public static class Extensions
{
    public static ICoreServiceBuider AddCapEventBus(this ICoreServiceBuider serviceBuider)
    {
        var eventBusOptions = AppCore.GetConfig<EventBusOptions>();
      
        return serviceBuider.AddCapEventBus(capOption =>
        {
            if (eventBusOptions.UseRabbitMQ)
            {
                capOption.UseRabbitMQ(option =>
                {
                    option.HostName = eventBusOptions.HostName;
                    option.VirtualHost = eventBusOptions.VirtualHost;
                    option.Port = eventBusOptions.Port;
                    option.UserName = eventBusOptions.UserName;
                    option.Password = eventBusOptions.Password;
                    option.ExchangeName = eventBusOptions.ExchangeName;
                });
            }
            else
            {
                capOption.UseInMemoryStorage();
                capOption.UseInMemoryMessageQueue();
            }

            //默认值：60 秒,重试 & 间隔
            //在默认情况下，重试将在发送和消费消息失败的 4分钟后 开始，这是为了避免设置消息状态延迟导致可能出现的问题。
            //发送和消费消息的过程中失败会立即重试 3 次，在 3 次以后将进入重试轮询，此时 FailedRetryInterval 配置才会生效。
            capOption.FailedRetryInterval = 60;
            //默认值：50,重试的最大次数。当达到此设置值时，将不会再继续重试，通过改变此参数来设置重试的最大次数。
            capOption.FailedRetryCount = 50;
            //默认值：NULL,重试阈值的失败回调。当重试达到 FailedRetryCount 设置的值的时候，将调用此 Action 回调
            //，你可以通过指定此回调来接收失败达到最大的通知，以做出人工介入。例如发送邮件或者短信。
            capOption.FailedThresholdCallback = (failed) =>
            {
                //todo
            };
            //默认值：24*3600 秒（1天后),成功消息的过期时间（秒）。
            //当消息发送或者消费成功时候，在时间达到 SucceedMessageExpiredAfter 秒时候将会从 Persistent 中删除，你可以通过指定此值来设置过期的时间。
            capOption.SucceedMessageExpiredAfter = 24 * 3600;
            //默认值：1,消费者线程并行处理消息的线程数，当这个值大于1时，将不能保证消息执行的顺序。
            capOption.ConsumerThreadCount = 1;
            //Dashboard
            capOption.UseDashboard(x =>
            {
                x.PathMatch = "/cap";
            });
        });
    }

    public static ICoreServiceBuider AddCapEventBus(this ICoreServiceBuider serviceBuider, Action<CapOptions> action)
    {
        var options = AppCore.GetConfig<AppOptions>();

        var assemblies = Reflections.GetAssemblyList(options.Modules);

        serviceBuider.Services
            .AddSingleton<IEventBusPublisher, CapPublisher>()
            .AddCap(action)
            .AddSubscribeFilter<DefaultCapFilter>()
            .AddSubscriberAssembly(assemblies);

        return serviceBuider;
    }
}
