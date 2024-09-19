namespace YK.Console.Business.TenantPackages;

public class SaveTenantPackageRequest : IRequest<bool>
{
    /// <summary>
    /// 租户Id
    /// </summary>
    [Required]
    public Guid TenantId { get; set; }

    /// <summary>
    /// 套餐id集合
    /// </summary>
    [Required]
    public List<Guid> PackageIds { get; set; }
}


/// <summary>
/// 依据租户获取租户套餐
/// </summary>
public class TenantPackageSearchByTenantRequest:IRequest<List<TenantPackageOutput>>
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public Guid TenantId { get; set; }
}