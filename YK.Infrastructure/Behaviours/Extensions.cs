using Microsoft.Extensions.DependencyInjection;

namespace YK.Infrastructure.Behaviours;

public static class Extensions
{
    /// <summary>
    /// 注入MediatR与Behavior
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatRBehavior(this IServiceCollection services, Action<MediatRBehaviorConfig> action)
    {
        var config = new MediatRBehaviorConfig();
        action(config);

        var requestTypes = Reflections.GetImplementations<IBaseRequest>(config.Assemblies).ToList();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(config.Assemblies);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            requestTypes.ForEach(t =>
            {
                var interfaces = t.GetInterfaces();
                var types = interfaces[0].GenericTypeArguments;
                cfg.AddBehavior(typeof(IPipelineBehavior<,>).MakeGenericType(t, types[0]), config.BehaviorType.MakeGenericType(t, types[0]));
            });
        });

        return services;
    }
}
