namespace YK.Console.Business.DataDicts;

/// <summary>
/// 字典输入
/// </summary>
public class DataDictInput
{
    /// <summary>
    /// 字典父级编码
    /// </summary>
    [MaxLength(32)]
    public string? ParentCode { get; set; }

    /// <summary>
    /// 字典唯一编码
    /// </summary>
    [Required,MaxLength(32)]
    public string Code { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    [Required,MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public int Sort { get; set; }

    /// <summary>
    /// 样式
    /// </summary>
    [MaxLength(64)]
    public string? ThemeStyle { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }
}
