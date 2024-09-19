using System.Text.RegularExpressions;

namespace YK.DynamicApi.Options;

/// <summary>
/// Swagger配置
/// </summary>
public class SwaggerOptions
{
    /// <summary>
    /// 启用
    /// </summary>
    public bool Enable { get; set; } = false;

    /// <summary>
    /// 启用枚举架构过滤器
    /// </summary>
    public bool EnableEnumSchemaFilter { get; set; } = true;

    /// <summary>
    /// 启用接口排序文档过滤器
    /// </summary>
    public bool EnableOrderTagsDocumentFilter { get; set; } = true;

    /// <summary>
    /// 启用枚举属性名
    /// </summary>
    public bool EnableJsonStringEnumConverter { get; set; } = false;

    /// <summary>
    /// 启用SchemaId命名空间
    /// </summary>
    public bool EnableSchemaIdNamespace { get; set; } = false;

    /// <summary>
    /// 程序集列表
    /// </summary>
    public string[] AssemblyNameList { get; set; }

    private string _RoutePrefix = "swagger";

    /// <summary>
    /// 访问地址
    /// </summary>
    public string RoutePrefix { get => Regex.Replace(_RoutePrefix, "^\\/+|\\/+$", ""); set => _RoutePrefix = value; }

    /// <summary>
    /// 地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 项目列表
    /// </summary>
    public List<SwaggerProjectOptions> Projects { get; set; }

    /// <summary>
    /// Swagger页脚配置
    /// </summary>
    public SwaggerFooterOptions Footer { get; set; }
}

/// <summary>
/// Swagger页脚配置
/// </summary>
public class SwaggerFooterOptions
{
    /// <summary>
    /// 启用
    /// </summary>
    public bool Enable { get; set; } = false;

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
}

/// <summary>
/// Swagger项目
/// </summary>
public class SwaggerProjectOptions
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
}