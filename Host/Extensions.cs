namespace YK.Host;

public static class Extensions
{
    internal static async Task BuildrHostAsync(this WebApplicationBuilder builder)
    {
        builder.AddConfigurations();

        var options = builder.Configuration.GetOptions<AppOptions>();
        var apiOptions = builder.Configuration.GetOptions<OpenApiOptions>();

        builder.Services.AddControllers(opt =>
        {
            opt.Filters.Add<InputValidateFilter>();
            opt.Filters.Add<PermissionValidatorFilter>();
            if (options.FormatResult is true) opt.Filters.Add<FormatResultFilter>();
            if (apiOptions.Swagger.Enable is true) opt.Conventions.Add(new ApiGroupConvention());
        }).AddNewtonsoftJson();

        var coreBuilder = builder.AddInfrastructure();

        if (apiOptions.ApiUI.Enable is true)
        {
            coreBuilder.AppBuilder += app =>
            {
                app.UseApiUI(options =>
                {
                    options.RoutePrefix = apiOptions.ApiUI.RoutePrefix;
                    apiOptions.Swagger.Projects?.ForEach(project =>
                    {
                        options.SwaggerEndpoint($"/{options.RoutePrefix}/swagger/{project.Code.ToLower()}/swagger.json", project.Name);
                    });
                });
            };
        }

        var app = builder.Build();

        app.UseInfrastructure();

        await app.RunAsync();
    }
}
