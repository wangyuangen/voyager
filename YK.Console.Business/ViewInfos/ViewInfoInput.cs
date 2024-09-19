namespace YK.Console.Business.ViewInfos;

public class ViewInfoInput
{
    /// <summary>
    /// 视图路由命名,ts中声明
    /// </summary>
    [Required, MaxLength(64)]
    public string RouteName { get; set; }

    /// <summary>
    /// 视图名称
    /// </summary>
    [Required, MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    [Required, MaxLength(128)]
    public string Path { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

    /// <summary>
    /// 是否缓存
    /// </summary>
    [Required]
    public bool IsKeepAlive { get; set; } = false;

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; } = EnabledStatusEnum.Disabled;

    /// <summary>
    /// 上级
    /// </summary>
    public Guid? ParentId { get; set; }
}
