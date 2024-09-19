namespace YK.DynamicApi.Options;

/// <summary>
/// 开放接口配置
/// </summary>
public class OpenApiOptions
{
    /// <summary>
    /// Swagger配置
    /// </summary>
    public SwaggerOptions Swagger { get; set; }

    /// <summary>
    /// 接口UI配置
    /// </summary>
    public ApiUIOptions ApiUI { get; set; }

    /// <summary>
    /// 开启性能分析器
    /// </summary>
    public bool MiniProfiler { get; set; }
}

