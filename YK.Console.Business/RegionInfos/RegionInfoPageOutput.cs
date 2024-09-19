using YK.Console.Core.Enums;

namespace YK.Console.Business.RegionInfos;

public class RegionInfoPageOutput:BaseOutput
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
}
