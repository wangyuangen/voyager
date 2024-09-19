using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using YK.Core.Commons;
using YK.Core.Contract;
using YK.Core.Enums;

namespace YK.Authorize;

public class ResponseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public ResponseAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger,
        UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        var _serialization = AppCore.GetRequiredService<ISerializerService>();
        Response.ContentType = "application/json";
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        var result = ResultOutput.NotOk(HttpStatusCode.Status401Unauthorized.Description());
        result.Code = HttpStatusCode.Status401Unauthorized.Value();
        await Response.WriteAsync(_serialization.Serialize(result));
    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        var _serialization = AppCore.GetRequiredService<ISerializerService>();
        Response.ContentType = "application/json";
        Response.StatusCode = StatusCodes.Status403Forbidden;
        var result = ResultOutput.NotOk(HttpStatusCode.Status403Forbidden.Description());
        result.Code = HttpStatusCode.Status403Forbidden.Value();
        await Response.WriteAsync(_serialization.Serialize(result));
    }
}
