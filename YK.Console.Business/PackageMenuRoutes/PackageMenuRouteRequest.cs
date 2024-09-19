namespace YK.Console.Business.PackageMenuRoutes;

/// <summary>
/// 保存套餐菜单
/// </summary>
public class SavePackageMenuRouteRequest : IRequest<bool>
{
    /// <summary>
    /// 套餐Id
    /// </summary>
    [Required]
    public Guid PackageId { get; set; }

    /// <summary>
    /// 套餐菜单Id集合
    /// </summary>
    [Required]
    public List<Guid> MenuRouteIds { get; set; }
}


/// <summary>
/// 依据套餐Id集合查询套餐菜单列表
/// </summary>
public class PackageMenuRouteSearchByPackagesRequest:IRequest<List<PackageMenuRouteOutput>>
{
    /// <summary>
    /// 套餐Id集合
    /// </summary>
    public List<Guid> PackageIds { get; set; }
}