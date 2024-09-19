using Lazy.SlideCaptcha.Core;

namespace YK.Console.Business.Captchas;

/// <summary>
/// 滑块验证校验
/// </summary>
public class CaptchaValidRequest : IRequest<ValidateResult>
{
    /// <summary>
    /// 滑块验证Id
    /// </summary>
    [Required]
    public string CaptchaId { get; set; }

    /// <summary>
    /// 滑动轨迹
    /// </summary>
    [Required]
    public SlideTrack Track { get; set; }

    /// <summary>
    /// 验证成功时从Storage删除
    /// </summary>
    public bool RemoveWhileSuccess { get; set; } = false;
}


/// <summary>
/// 发送短信验证码
/// </summary>
public class SendSmsCodeRequest : IRequest<string>
{
    /// <summary>
    /// 手机号
    /// </summary>
    [Required]
    public string Mobile { get; set; }

    /// <summary>
    /// 验证码Id
    /// </summary>
    public string? CodeId { get; set; }

    /// <summary>
    /// 滑块验证Id
    /// </summary>
    [Required]
    public string CaptchaId { get; set; }

    /// <summary>
    /// 滑动轨迹
    /// </summary>
    [Required]
    public SlideTrack Track { get; set; }
}