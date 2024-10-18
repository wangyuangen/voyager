namespace YK.Console.Business.ApiInfos;

public class ApiInfoOutput : BaseOutput
{
    /// <summary>
    /// 接口名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 接口路径
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 接口请求方法
    /// </summary>
    public string? HttpMethod { get; set; }

    /// <summary>
    /// 接口描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 上级
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }
}
