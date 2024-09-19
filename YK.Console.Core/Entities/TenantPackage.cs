using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 租户套餐
/// </summary>
[DataSeederOrder(20)]
public class TenantPackage : BaseTenantEntity
{
    /// <summary>
    /// 租户
    /// </summary>
    public virtual TenantInfo Tenant { get; set; }

    /// <summary>
    /// 套餐Id
    /// </summary>
    public Guid PackageId { get; set; }

    /// <summary>
    /// 套餐
    /// </summary>
    public virtual PackageInfo Package { get; set; }
}
