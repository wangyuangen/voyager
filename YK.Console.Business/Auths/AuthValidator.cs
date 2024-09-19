using YK.Console.Business.Captchas;

namespace YK.Console.Business.Auths;

internal class AccountLoginValidator : AbstractValidator<AccountLoginRequest>
{
    public AccountLoginValidator(ISender sender)
    {
        RuleFor(x => x)
            .MustAsync(async (inst, ct) =>
            {
                var appOptions = AppCore.GetConfig<AppOptions>();
                if (appOptions.EnableSlideCaptcha)
                {
                    if (inst.CaptchaId.IsNullOrEmpty() || inst.Track.IsNull()) return false;

                    var captchaValidRequest = inst.Adapt<CaptchaValidRequest>();
                    captchaValidRequest.RemoveWhileSuccess = true;
                    var result = await sender.Send(captchaValidRequest, ct);
                    return result.Result == Lazy.SlideCaptcha.Core.ValidateResult.ValidateResultType.Success;
                }
                return true;
            })
            .WithMessage(_ => "验证失败");
    }
}
