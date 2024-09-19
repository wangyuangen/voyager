namespace YK.Console.Business.TenantPackages;

public class TenantPackageOutput:BaseOutput
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// 套餐Id
    /// </summary>
    public Guid PackageId { get; set; }
}
