using Microsoft.Extensions.DependencyInjection;
using YK.Core.Options;
using YK.Core;
using YK.CRM.Core.DbContext;
using YK.CRM.Core.Interceptors;
using YK.CRM.Core.Options;
using YK.Infrastructure.Behaviours;
using System.Reflection;
using YK.Core.Extensions;

namespace YK.CRM;

public class Extensions : IModuleExtension
{
    public void Register(ICoreServiceBuider serviceBuider)
    {
        var appOptions = AppCore.GetConfig<AppOptions>();
        var assemblies = Assembly.GetExecutingAssembly().GetCurrentWithReferenceAssembly(appOptions.Modules);

        //注入Crm数据库与仓储
        serviceBuider.AddOrm<CrmDbContext, CrmDbOptions>(typeof(CrmDbRepository<>));

        serviceBuider.Services
            //注入Crm工作单元处理器
            .AddScoped<ICrmUnitOfWork, CrmUnitOfWork>()
            //注入接口事务拦截器
            .AddScoped<CrmTransactionInterceptor>()
            .AddScoped<CrmTransactionAsyncInterceptor>()
            .AddTransactionInterceptors(config =>
            {
                config.InterceptorTypes.Add(typeof(CrmTransactionInterceptor));
                config.Assemblies = assemblies;
            })
            //注入行为管道事务拦截器
            .AddMediatRBehavior(config =>
            {
                config.Assemblies = assemblies;
                config.BehaviorType = typeof(CrmUnitOfWorkBehavior<,>);
            });
    }
}
