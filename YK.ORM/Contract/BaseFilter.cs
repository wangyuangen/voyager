namespace YK.ORM.Contract;

public class BaseFilter
{
    /// <summary>
    /// 模糊查询条件
    /// </summary>
    public AdvancedSearch? Search { get; set; }

    /// <summary>
    /// 模糊查询关键字
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 筛选过滤条件
    /// </summary>
    public AdvancedFilter? Filter { get; set; }
}
