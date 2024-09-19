using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using YK.Console.Core.Options;
using YK.Console.Core.DbContext;
using YK.Console.Core.Entities;
using YK.Console.Core.Interceptors;
using YK.Core.Options;
using YK.Infrastructure.Behaviours;
using System.Reflection;
using YK.Core.Extensions;

namespace YK.Console;

public class Extensions:IModuleExtension
{
    public void Register(ICoreServiceBuider builder)
    {
        var appOptions = AppCore.GetConfig<AppOptions>();
        var assemblies = Assembly.GetExecutingAssembly().GetCurrentWithReferenceAssembly(appOptions.Modules);

        //注入滑块验证组件
        builder.Services.AddSlideCaptcha(AppCore.Configuration);

        //注入Console数据库与仓储
        builder.AddOrm<ConsoleDbContext, ConsoleDbOptions>(typeof(ConsoleDbRepository<>));

        builder.Services
            //密码hash校验
            .AddSingleton(typeof(IPasswordHasher<UserInfo>), typeof(PasswordHasher<UserInfo>))
            //注入Console工作单元处理器
            .AddScoped<IConsoleUnitOfWork, ConsoleUnitfOfWork>()
            //注入接口事务拦截器
            .AddScoped<ConsoleTransactionInterceptor>()
            .AddScoped<ConsoleTransactionAsyncInterceptor>()
            .AddTransactionInterceptors(config =>
            {
                config.InterceptorTypes.Add(typeof(ConsoleTransactionInterceptor));
                config.Assemblies = assemblies;
            })
            //注入行为管道事务拦截器
            .AddMediatRBehavior(config =>
            {
                config.Assemblies = assemblies;
                config.BehaviorType = typeof(ConsoleUnitOfWorkBehavior<,>);
            });
    }
}
