namespace YK.Core.Commons.Models;

/// <summary>
/// swagger接口文档信息
/// </summary>
public class SwaggerApiInfoDto
{
    /// <summary>
    /// 接口名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 接口地址
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 父级路径
    /// </summary>
    public string ParentPath { get; set; }

    /// <summary>
    /// 接口提交方法
    /// </summary>
    public string HttpMethod { get; set; }

    /// <summary>
    /// 接口描述
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
}
