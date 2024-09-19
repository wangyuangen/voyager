using YK.ORM.Enums;

namespace YK.ORM.Contract;

/// <summary>
/// 高级过滤
/// </summary>
public class AdvancedFilter
{
    /// <summary>
    /// 过滤条件
    /// </summary>
    public FilterLogicEnum? Logic { get; set; }

    /// <summary>
    /// 筛选过滤条件子项
    /// </summary>
    public IEnumerable<AdvancedFilter>? Filters { get; set; }

    /// <summary>
    /// 字段名称
    /// </summary>
    public string? Field { get; set; }

    /// <summary>
    /// 逻辑运算符
    /// </summary>
    public FilterOperatorEnum? Operator { get; set; }

    /// <summary>
    /// 字段值
    /// </summary>
    public object? Value { get; set; }
}
