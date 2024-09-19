namespace YK.Console.Business.DataDicts;

/// <summary>
/// 字典输出
/// </summary>
public class DataDictOutput:AuditableOutput
{
    /// <summary>
    /// 字典父级编码
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 字典唯一编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 样式
    /// </summary>
    public string? ThemeStyle { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

}
