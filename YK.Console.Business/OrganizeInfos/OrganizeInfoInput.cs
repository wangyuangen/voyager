using YK.Console.Core.Enums;

namespace YK.Console.Business.OrganizeInfos;

public class OrganizeInfoInput
{
    /// <summary>
    /// 父级
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 机构名称
    /// </summary>
    [Required,MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 机构类型
    /// </summary>
    [Required]
    public OrganizeTypeEnum OrganizeType { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required]
    public int Sort { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    [Required, MaxLength(32)]
    public string RegionCode { get; set; }

    /// <summary>
    /// 区域
    /// </summary>
    [Required, MaxLength(256)]
    public string RegionText { get; set; }
}
