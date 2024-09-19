namespace YK.Console.Business.Tenants;

/// <summary>
/// 租户输入
/// </summary>
public class TenantInput
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required, MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    [Required, MaxLength(64)]
    public string CompanyName { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [Required, MaxLength(16)]
    public string ContactPerson { get; set; }

    /// <summary>
    /// 联系手机
    /// </summary>
    [Required, MaxLength(16)]
    public string ContactMobile { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

    /// <summary>
    /// 有效期限
    /// </summary>
    [Required]
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    [Required,MaxLength(32)]
    public string RegionCode { get; set; }

    /// <summary>
    /// 区域
    /// </summary>
    [Required,MaxLength(256)]
    public string RegionText { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }
}
