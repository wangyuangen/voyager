using YK.Module.Core.Contracts;
using YK.Module.Core.Enums;

namespace YK.Module.Core.Models;

/// <summary>
/// 机构输出
/// </summary>
public class OrganizeInfoOutput: AuditableOutput
{
    /// <summary>
    /// 父级
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 机构名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 机构类型
    /// </summary>
    public OrganizeTypeEnum OrganizeType { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    public string RegionCode { get; set; }

    /// <summary>
    /// 区域
    /// </summary>
    public string RegionText { get; set; }
}
