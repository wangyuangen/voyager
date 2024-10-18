namespace YK.Console.Business.ViewInfos;

public class ViewInfoOutput : BaseOutput
{
    /// <summary>
    /// 视图路由命名,ts中声明
    /// </summary>
    public string RouteName { get; set; }

    /// <summary>
    /// 视图名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool IsKeepAlive { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 上级
    /// </summary>
    public Guid? ParentId { get; set; }
}
