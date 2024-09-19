namespace YK.Console.Business.PermissionGroups;

public class PermissionGroupInput
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required,MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

    /// <summary>
    /// 数权范围
    /// </summary>
    [Required]
    public DataPermissionScope Scope { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public Guid? ParentId { get; set; }
}
