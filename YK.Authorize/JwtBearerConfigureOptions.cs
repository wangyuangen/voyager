using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YK.Authorize.Options;
using YK.Core.Consts;

namespace YK.Authorize;

public class JwtBearerConfigureOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(JwtBearerOptions options)
    {
        Configure(string.Empty, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme) return;

        var authOptions = AppCore.GetConfig<AuthorizeOptions>();

        var bytes = Encoding.UTF8.GetBytes(authOptions.SecurityKey);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authOptions.Issuer,
            ValidAudience = authOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(bytes),
            ClockSkew = TimeSpan.FromSeconds(10)
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = OnMessageReceivedAsync,
            //OnAuthenticationFailed = OnAuthenticationFailedAsync,
            //OnChallenge = OnChallengeAsync,
            //OnTokenValidated = OnTokenValidatedAsync,
            //OnForbidden = OnForbiddenAsync
        };
    }

    ///// <summary>
    ///// 禁止
    ///// </summary>
    ///// <param name="context"></param>
    ///// <returns></returns>
    //private Task OnForbiddenAsync(ForbiddenContext context)
    //{
    //    return Task.CompletedTask;
    //}

    ///// <summary>
    ///// token失效后
    ///// </summary>
    ///// <param name="context"></param>
    ///// <returns></returns>
    //private Task OnTokenValidatedAsync(TokenValidatedContext context)
    //{
    //    return Task.CompletedTask;
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="context"></param>
    ///// <returns></returns>
    //private Task OnChallengeAsync(JwtBearerChallengeContext context)
    //{
    //    return Task.CompletedTask;
    //}

    ///// <summary>
    ///// 认证失败后
    ///// </summary>
    ///// <param name="context"></param>
    ///// <returns></returns>
    //private Task OnAuthenticationFailedAsync(AuthenticationFailedContext context)
    //{
    //    return Task.CompletedTask;
    //}

    /// <summary>
    /// 收到请求消息后
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private Task OnMessageReceivedAsync(MessageReceivedContext context)
    {
        //针对signalr请求的处理
        string? token = string.Empty;

        if (context.Request.Query.Keys.Contains(QuerystringKeys.AccessToken))
        {
            token = context.Request.Query[QuerystringKeys.AccessToken];
        }

        if (!string.IsNullOrWhiteSpace(token)) context.Token = token;

        return Task.CompletedTask;
    }
}
