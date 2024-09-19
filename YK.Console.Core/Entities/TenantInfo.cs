using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 租户信息
/// </summary>
[DataSeederOrder(10)]
public class TenantInfo: AuditableSoftDeleteEntity
{
    /// <summary>
    /// 租户名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string ContactPerson { get; set; }

    /// <summary>
    /// 联系手机
    /// </summary>
    public string ContactMobile { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 租户类型
    /// </summary>
    public TenantTypeEnum TenantType { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 有效期限
    /// </summary>
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    public string RegionCode { get; set; }

    /// <summary>
    /// 区域
    /// </summary>
    public string RegionText { get; set; }

    /// <summary>
    /// 租户套餐列表
    /// </summary>
    public virtual List<TenantPackage> TenantPackages { get; set; }

    /// <summary>
    /// 租户机构列表
    /// </summary>
    public virtual List<OrganizeInfo> OrganizeInfos { get; set; }

    public TenantInfo Update(string? name =null, string? companyName = null, string? contactPerson = null, string? contactMobile = null, string? remark = null, 
        TenantTypeEnum? tenantType =null, EnabledStatusEnum? enabled = null, DateTime? expiryDate = null,string? regionCode = null,string? regionText=null)
    {
        if (name is not null && Name?.Equals(name) is not true)
            Name = name;
        if(companyName is not null && CompanyName?.Equals(companyName) is not true)
            CompanyName = companyName;
        if(contactPerson is not null && ContactPerson?.Equals(contactPerson) is not true)
            ContactPerson = contactPerson;
        if(contactMobile is not null && ContactMobile?.Equals(contactMobile) is not true)
            ContactMobile = contactMobile;
        if(remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if (tenantType.HasValue && TenantType != tenantType)
            TenantType = tenantType.Value;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        if (expiryDate.HasValue && ExpiryDate != expiryDate)
            ExpiryDate = expiryDate.Value;
        if (regionCode is not null && RegionCode?.Equals(regionCode) is not true)
            RegionCode = regionCode;
        if (regionText is not null && RegionText?.Equals(regionText) is not true)
            RegionText = regionText;
        return this;
    }
}
