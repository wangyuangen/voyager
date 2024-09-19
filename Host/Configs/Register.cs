namespace YK.Host;

internal static class Register
{
    internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configs";
        var env = builder.Environment.EnvironmentName;
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/database.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/database.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/appsettings.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/appsettings.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/logger.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/logger.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/openapi.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/openapi.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/authorize.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/authorize.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/hangfire.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/hangfire.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/captcha.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/captcha.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/cache.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/cache.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/signalr.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/signalr.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/cors.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/cors.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/ipratelimit.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/ipratelimit.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/fileupload.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/fileupload.{env}.json", true, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/eventbus.json", false, true);
        builder.Configuration.AddJsonFile($"{configurationsDirectory}/eventbus.{env}.json", true, true);
        return builder;
    }
}
