using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YK.Core.Attributes;

namespace YK.Infrastructure.Interceptors;

public static class Extensions
{
    /// <summary>
    /// 注入接口级事务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddTransactionInterceptors(this IServiceCollection services, Action<TransactionInterceptorsConfig> configAction)
    {
        var configuration = new TransactionInterceptorsConfig();

        configAction.Invoke(configuration);

        services.TryAddSingleton(new ProxyGenerator());

        var exportTypes = configuration.Assemblies.SelectMany(x=>x.GetExportedTypes());
        var serviceTypes = exportTypes.Where(type => type.IsInterface && type.CustomAttributes.Any(d=>d.AttributeType == typeof(UnitOfWorkAttribute))).ToList();

        serviceTypes.ForEach(serviceType =>
        {
            var implType = exportTypes.FirstOrDefault(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true));
            if (implType is null)
                return;

            services.AddScoped(implType);
            services.AddScoped(serviceType, provider =>
            {
                var interfaceToProxy = serviceType;
                var target = provider.GetService(implType);
                var interceptors = configuration.InterceptorTypes.ConvertAll(interceptorType => provider.GetService(interceptorType) as IInterceptor).ToArray();
                var proxyGenerator = provider.GetRequiredService<ProxyGenerator>();
                var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(interfaceToProxy, target, interceptors);
                return proxy;
            });
        });

        return services;
    }
}
