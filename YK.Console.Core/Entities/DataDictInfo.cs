using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 数据字典
/// </summary>
[DataSeederOrder(10)]
public class DataDictInfo:BaseSoftDeleteEntity
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentCode"></param>
    /// <param name="code"></param>
    /// <param name="name"></param>
    /// <param name="sort"></param>
    /// <param name="themeStyle"></param>
    /// <param name="remark"></param>
    /// <param name="enabled"></param>
    /// <returns></returns>
    public DataDictInfo Update(string? parentCode = null,string? code = null, string? name = null, 
        int? sort = null,string? themeStyle = null, string? remark = null, EnabledStatusEnum? enabled = null)
    {
        if (parentCode is not null && ParentCode?.Equals(parentCode) is not true)
            ParentCode = parentCode;
        if (code is not null && Code?.Equals(code) is not true)
            Code = code;
        if(name is not null && Name?.Equals(name) is not true)
            Name = name;
        if(sort.HasValue && Sort!=sort)
            Sort = sort.Value;
        if (themeStyle is not null && ThemeStyle?.Equals(themeStyle) is not true)
            ThemeStyle = themeStyle;
        if(remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        return this;
    }
}
