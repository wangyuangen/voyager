namespace YK.ORM.Contract;

/// <summary>
/// 模糊查询
/// </summary>
public class AdvancedSearch
{
    /// <summary>
    /// 字段名称集合
    /// </summary>
    public List<string> Fields { get; set; }
    /// <summary>
    /// 关键字
    /// </summary>
    public string? Keyword { get; set; }
}
