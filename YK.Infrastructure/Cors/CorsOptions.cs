namespace YK.Infrastructure.Cors;

/// <summary>
/// 跨域设置
/// </summary>
public class CorsOptions
{
    /// <summary>
    /// web白名单
    /// </summary>
    public string[]? WebWhiteList { get; set; }
    
    /// <summary>
    /// app白名单
    /// </summary>
    public string[]? AppWhiteList { get; set; }
}
