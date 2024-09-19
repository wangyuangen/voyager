using YK.Console.Core.Enums;
using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 机构
/// </summary>
[DataSeederOrder(20)]
public class OrganizeInfo : AuditableTenantSoftDeleteEntity
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

    /// <summary>
    /// 
    /// </summary>
    public virtual TenantInfo Tenant { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual List<UserStaffOrg> UserStaffOrgs { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual List<UserStaffInfo> UserStaffInfos { get; set; }

    public OrganizeInfo Update(Guid? parentId = null, string? name = null, OrganizeTypeEnum? organizeType = null, EnabledStatusEnum? enabled = null,
        string? remark = null, int? employeeCount = null, int? sort = null, string? regionCode = null, string? regionText = null)
    {
        if(parentId.HasValue && ParentId!=parentId)
            ParentId = parentId.Value;
        if (name is not null && Name?.Equals(name) is not true)
            Name = name;
        if (organizeType.HasValue && OrganizeType != organizeType)
            OrganizeType = organizeType.Value;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        if(remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if (sort.HasValue && Sort != sort)
            Sort = sort.Value;
        if (regionCode is not null && RegionCode?.Equals(regionCode) is not true)
            RegionCode = regionCode;
        if (regionText is not null && RegionText?.Equals(regionText) is not true)
            RegionText = regionText;
        return this;
    }
}
