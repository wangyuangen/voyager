using System.Text.RegularExpressions;

namespace YK.DynamicApi.Options;

/// <summary>
/// Api ui配置
/// </summary>
public class ApiUIOptions
{
    /// <summary>
    /// 启用
    /// </summary>
    public bool Enable { get; set; } = false;

    private string _RoutePrefix = "";

    /// <summary>
    /// 访问地址
    /// </summary>
    public string RoutePrefix { get => Regex.Replace(_RoutePrefix, "^\\/+|\\/+$", ""); set => _RoutePrefix = value; }
}
