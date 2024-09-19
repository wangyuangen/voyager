namespace YK.Console.Business.Roles;

/// <summary>
/// 角色
/// </summary>
public class RoleInfoInput
{
    /// <summary>
    /// 角色名称
    /// </summary>
    [Required,MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

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
}
