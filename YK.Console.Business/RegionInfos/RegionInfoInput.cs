using YK.Console.Core.Enums;

namespace YK.Console.Business.RegionInfos;

public class RegionInfoInput
{
    /// <summary>
    /// 上级地区代码
    /// </summary>
    [MaxLength(32)]
    public string? ParentCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required,MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    [Required]
    public RegionLevel Level { get; set; }

    /// <summary>
    /// 代码
    /// </summary>
    [Required, MaxLength(32)]
    public string Code { get; set; }

    /// <summary>
    /// 提取Url
    /// </summary>
    [MaxLength(128)]
    public string? Url { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required]
    public int Sort { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    [Required]
    public bool Hot { get; set; } = false;

    /// <summary>
    /// 启用
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }
}
