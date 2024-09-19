using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YK.Authorize.Abstractions;
using YK.Core.Authorize;

namespace YK.Authorize;

public static class Extensions
{
    /// <summary>
    /// 注入身份认证
    /// </summary>
    /// <param name="serviceBuider"></param>
    /// <returns></returns>
    public static ICoreServiceBuider AddAuthorize(this ICoreServiceBuider serviceBuider)
    {
        serviceBuider.Services
            .AddJwtAuthorize();

        return serviceBuider;
    }

    /// <summary>
    /// 添加Jwt认证
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    private static AuthenticationBuilder AddJwtAuthorize(this IServiceCollection services)
    {
        return services
           .AddSingleton<ITokenBuilder, TokenBuilder>()
           .AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerConfigureOptions>()
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(ResponseAuthenticationHandler); //401
                options.DefaultForbidScheme = nameof(ResponseAuthenticationHandler);    //403
            })
          .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!)
          .AddScheme<AuthenticationSchemeOptions, ResponseAuthenticationHandler>(nameof(ResponseAuthenticationHandler), null!);
    }
}
