using YK.Core.Enums;

namespace YK.Core.Options;

/// <summary>
/// 应用配置项
/// </summary>
public class AppOptions
{
    /// <summary>
    /// 模块程序集
    /// </summary>
    public string[]? Modules { get; set; }

    /// <summary>
    /// 是否启用Http日志
    /// </summary>
    public bool EnableHttpLogging { get; set; }

    /// <summary>
    /// 是否开启结果格式化
    /// </summary>
    public bool FormatResult { get; set; }

    /// <summary>
    /// 默认密码
    /// </summary>
    public string DefaultPassword { get; set; }

    /// <summary>
    /// 密码加密类型
    /// </summary>
    public PasswordEncryptType PasswordEncryptType { get; set; }

    /// <summary>
    /// 启用登录滑块验证
    /// </summary>
    public bool EnableSlideCaptcha { get; set; }
}
