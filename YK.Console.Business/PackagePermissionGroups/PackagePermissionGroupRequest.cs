namespace YK.Console.Business.PackagePermissionGroups;

/// <summary>
/// 保存套餐权限组
/// </summary>
public class SavePackagePermissionGroupRequest : IRequest<bool>
{
    /// <summary>
    /// 套餐Id
    /// </summary>
    [Required]
    public Guid PackageId { get; set; }

    /// <summary>
    /// 权限组Id集合
    /// </summary>
    public List<Guid> PermissionGroupIds { get; set; }
}

/// <summary>
/// 依据套餐Id集合查询套餐权限组列表
/// </summary>
public class PkgPermissionGroupSearchByPkgIdsRequest:IRequest<List<PackagePermissionGroupOutput>>
{
    /// <summary>
    /// 套餐Id集合
    /// </summary>
    public List<Guid> PackageIds { get; set; }
}