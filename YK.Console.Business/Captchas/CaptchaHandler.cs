using Lazy.SlideCaptcha.Core;
using Lazy.SlideCaptcha.Core.Storage;
using IValidator = Lazy.SlideCaptcha.Core.Validator.IValidator;

namespace YK.Console.Business.Captchas;

/// <summary>
/// 滑块验证校验
/// </summary>
internal class CaptchaValidHandler(IStorage storage, IValidator validator) : IRequestHandler<CaptchaValidRequest, ValidateResult>
{
    private readonly IStorage _storage = storage;
    private readonly IValidator _validator = validator;

    public Task<ValidateResult> Handle(CaptchaValidRequest request, CancellationToken cancellationToken)
    {
        var captchaValidateData = _storage.Get<CaptchaValidateData>(request.CaptchaId);
        if (captchaValidateData.IsNull()) return Task.FromResult(ValidateResult.Timeout());

        var success = _validator.Validate(request.Track, captchaValidateData);

        if (!success || (success && request.RemoveWhileSuccess))
        {
            _storage.Remove(request.CaptchaId);
        }

        var result = success ? ValidateResult.Success() : ValidateResult.Fail();

        return Task.FromResult(result);
    }
}

internal class SendSmsCodeHandler : IRequestHandler<SendSmsCodeRequest, string>
{
    public Task<string> Handle(SendSmsCodeRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
