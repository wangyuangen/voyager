using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YK.Core;
using YK.ORM.Abstractions;
using YK.ORM.Options;
using YK.ORM.Presistence;
using YK.ORM.Specification;

namespace YK.ORM.Persistence;

public static class Extensions
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Extensions));

    /// <summary>
    /// 使用数据库
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="dbProvider"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
    {
        return dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql =>
                 builder.UseNpgsql(connectionString, e =>
                     e.MigrationsAssembly("YK.Migrators.PostgreSQL")),

            DbProviderKeys.SqlServer =>
                 builder.UseSqlServer(connectionString, e =>
                     e.MigrationsAssembly("YK.Migrators.MSSQL")),

            DbProviderKeys.MySql =>
                 builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), e =>
                     e.MigrationsAssembly("YK.Migrators.MySQL")
                      .SchemaBehavior(MySqlSchemaBehavior.Ignore)),

            DbProviderKeys.Oracle =>
                 builder.UseOracle(connectionString, e =>
                     e.MigrationsAssembly("YK.Migrators.Oracle")),

            DbProviderKeys.SqLite =>
                 builder.UseSqlite(connectionString, e =>
                     e.MigrationsAssembly("YK.Migrators.SqLite")),

            _ => throw new InvalidOperationException($"Provider {dbProvider} is not supported.")
        };
    }

    /// <summary>
    /// 注入数据库上下文
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="IDbOptions"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddDbContext<TContext, IDbOptions>(this IServiceCollection services)
       where TContext : DbContext
        where IDbOptions : DatabaseOptions
    {
        var dbOptions = AppCore.GetConfig<IDbOptions>();
        _logger.Information("{option} current provider: {DatabaseProvider}", typeof(IDbOptions).Name, dbOptions.Provider);

        services.TryAddScoped<IDataPermissionEvaluator, DataPermissionEvaluator>();
        services.TryAddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.TryAddTransient<IConnectionStringValidator, ConnectionStringValidator<IDbOptions>>();
        services.TryAddScoped<IDbCommandInterceptor, CommandInterceptor>();
        services.AddDbContext<TContext>((sp, options) =>
        {
            //_logger.Information("use dbcontext with {options} current provider:{provider}", typeof(IDbOptions).Name, dbOptions.Provider);
            options.UseDatabase(dbOptions.Provider, dbOptions.ConnectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.AddInterceptors(sp.GetServices<IDbCommandInterceptor>());
        });

        return services;
    }

    /// <summary>
    /// 注入仓储
    /// </summary>
    /// <param name="services"></param>
    /// <param name="repositoryType"></param>
    /// <returns></returns>
    private static IServiceCollection AddRepositories(this IServiceCollection services, Type repositoryType)
    {
        var entityTypes = repositoryType.Assembly.GetExportedTypes()
            .Where(t => typeof(IEntity).IsAssignableFrom(t) && t.IsClass)
            .ToList();

        foreach (var entityType in entityTypes)
        {
            services.AddScoped(typeof(IRepository<>).MakeGenericType(entityType), repositoryType.MakeGenericType(entityType));
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(entityType), repositoryType.MakeGenericType(entityType));
            services.AddScoped(typeof(IRepositoryWithEvents<>).MakeGenericType(entityType), typeof(EventAddingRepositoryDecorator<>).MakeGenericType(entityType));
        }

        return services;
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder InitializerDatabases(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();
        foreach (var initializer in initializers)
        {
            initializer.MigrateAsync(CancellationToken.None).Wait();
        }

        var seeders = scope.ServiceProvider.GetServices<IDbDataSeeder>();
        foreach (var seeder in seeders)
        {
            seeder.SeederAsync(CancellationToken.None).Wait();
        }

        return app;
    }

    /// <summary>
    /// 注入ORM
    /// </summary>
    /// <typeparam name="TContext">数据库上下文</typeparam>
    /// <typeparam name="IDbOptions">数据库配置</typeparam>
    /// <param name="serviceBuider"></param>
    /// <param name="repositoryType">仓储类型</param>
    /// <returns></returns>
    public static ICoreServiceBuider AddOrm<TContext, IDbOptions>(this ICoreServiceBuider serviceBuider, Type repositoryType)
        where TContext : DbContext
        where IDbOptions : DatabaseOptions
    {
        serviceBuider.Services.AddDbContext<TContext, IDbOptions>()
            .AddRepositories(repositoryType);

        return serviceBuider;
    }
}
