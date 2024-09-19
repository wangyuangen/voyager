using Lazy.SlideCaptcha.Core;
using Microsoft.AspNetCore.Authorization;
using YK.Console.Business.Captchas;
using YK.Console.Core.Consts;

namespace YK.Console;

/// <summary>
/// 验证码服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup), Order(5)]
public class CaptchaService(ICaptcha _captcha, ISender _sender) : IDynamicApi
{
    /// <summary>
    /// 生成滑块验证数据
    /// </summary>
    /// <param name="captchaId">验证Id</param>
    /// <returns></returns>
    [AllowAnonymous]
    public CaptchaData Generate(string? captchaId = null)
        => _captcha.Generate(captchaId);

    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<ValidateResult> ValidateAsync(CaptchaValidRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 短信验证
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<string> SendSmsCodeAsync(SendSmsCodeRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);
}
