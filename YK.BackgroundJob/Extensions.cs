using Hangfire.Console;
using Hangfire.Console.Extensions;
using Hangfire.MySql;
using Hangfire.PostgreSql;
using Hangfire.SQLite;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YK.BackgroundJob.Abstractions;
using YK.BackgroundJob.Hangfire;
using YK.BackgroundJob.Options;

namespace YK.BackgroundJob;

public static class Extensions
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Extensions));

    public static ICoreServiceBuider AddHangfireJob(this ICoreServiceBuider serviceBuider)
    {
        serviceBuider.Services.AddScoped<IBackgroundJob, HangfireJobManager>();

        serviceBuider.Services.AddHangfireServer(options => AppCore.Configuration.GetSection("HangfireSettings:Server").Bind(options));

        serviceBuider.Services.AddHangfireConsoleExtensions();

        var storageSettings = AppCore.Configuration.GetOptions<HangfireStorageOptionss>("HangfireSettings:Storage");
        storageSettings.CheckNull(nameof(HangfireStorageOptionss));

        _logger.Information("Hangfire: Current Storage Provider : {provider}", storageSettings.StorageProvider);
        _logger.Information("For more Hangfire storage, visit https://www.hangfire.io/extensions.html");

        serviceBuider.Services.AddSingleton<JobActivator, HangfireActivator>();

        serviceBuider.Services.AddHangfire((provider, hangfireConfig) => hangfireConfig
            .UseDatabase(storageSettings.StorageProvider, storageSettings.ConnectionString, AppCore.Configuration)
            .UseFilter(new JobClientFilter())
            .UseFilter(new JobLogFilter())
            .UseConsole());

        serviceBuider.AppBuilder += app => app.UseHangfireDashboard(AppCore.Configuration);

        return serviceBuider;
    }

    private static IGlobalConfiguration UseDatabase(this IGlobalConfiguration hangfireConfig, string dbProvider, string connectionString, IConfiguration config) =>
             dbProvider.ToLowerInvariant() switch
             {
                 DbProviderKeys.Npgsql =>
                    hangfireConfig.UsePostgreSqlStorage(opt => config.GetSection("HangfireSettings:Storage:Options").Get<PostgreSqlStorageOptions>()),
                 DbProviderKeys.SqlServer =>
                     hangfireConfig.UseSqlServerStorage(connectionString, config.GetSection("HangfireSettings:Storage:Options").Get<SqlServerStorageOptions>()),
                 DbProviderKeys.SqLite =>
                     hangfireConfig.UseSQLiteStorage(connectionString, config.GetSection("HangfireSettings:Storage:Options").Get<SQLiteStorageOptions>()),
                 DbProviderKeys.MySql =>
                     hangfireConfig.UseStorage(new MySqlStorage(connectionString, config.GetSection("HangfireSettings:Storage:Options").Get<MySqlStorageOptions>())),
                 _ => throw new Exception($"Hangfire Storage Provider {dbProvider} is not supported.")
             };

    private static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration config)
    {
        var dashboardOptions = config.GetSection("HangfireSettings:Dashboard").Get<DashboardOptions>();
        if (dashboardOptions is null) throw new ArgumentException("Hangfire Dashboard is not configured.");
        dashboardOptions.Authorization = new[]
        {
           new HangfireCustomBasicAuthenticationFilter
           {
                User = config.GetSection("HangfireSettings:Credentials:User").Value,
                Pass = config.GetSection("HangfireSettings:Credentials:Password").Value
           }
        };

        return app.UseHangfireDashboard(config["HangfireSettings:Route"], dashboardOptions);
    }

}
