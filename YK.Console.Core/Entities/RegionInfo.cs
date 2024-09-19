using YK.Console.Core.Enums;

namespace YK.Console.Core.Entities;

/// <summary>
/// 行政区域
/// </summary>
public class RegionInfo:BaseEntity
{
    /// <summary>
    /// 上级区域代码
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public RegionLevel Level { get; set; }

    /// <summary>
    /// 区域代码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 拼音
    /// </summary>
    public string Pinyin { get; set; }

    /// <summary>
    /// 拼音首字母
    /// </summary>
    public string PinyinFirst { get; set; }

    /// <summary>
    /// 提取地址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 城乡分类代码
    /// </summary>
    public string? VilageCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    public bool Hot { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    public RegionInfo Update(string? parentCode=null,string? name = null, RegionLevel? level=null, string? code = null, string? pinyin = null, string? pinyinFirst = null,
        string? url = null, string? vilageCode = null, int? sort=null, bool? hot=null, EnabledStatusEnum? enabled = null)
    {
        if (parentCode is not null && ParentCode?.Equals(parentCode) is not true)
            ParentCode = parentCode;
        if (name is not null && Name?.Equals(name) is not true)
            Name = name;
        if (level.HasValue && Level != level)
            Level = level.Value;
        if(code is not null && Code?.Equals(code) is not true)
            Code = code;
        if(pinyin is not null && Pinyin?.Equals(pinyin) is not true)
            Pinyin = pinyin;
        if(pinyinFirst is not null && PinyinFirst?.Equals(pinyin) is not true)
            PinyinFirst = pinyinFirst;
        if(url is not null && Url?.Equals(url) is not true)
            Url = url;
        if(vilageCode is not null && VilageCode?.Equals(vilageCode) is not true)
            VilageCode = vilageCode;
        if (sort.HasValue && Sort != sort)
            Sort = sort.Value;
        if (hot.HasValue && Hot != hot)
            Hot = hot.Value;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        return this;
    }
}
