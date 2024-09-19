namespace YK.Console.Business.Tenants;

public class TenantOutput: AuditableOutput
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
}
