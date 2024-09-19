using YK.Console.Business.PermissionGroups;

namespace YK.Console.Business.PackagePermissionGroups;

public class PackagePermissionGroupOutput:BaseOutput
{
    /// <summary>
    /// 套餐Id
    /// </summary>
    public Guid PackageId { get; set; }

    /// <summary>
    /// 权限组Id
    /// </summary>
    public Guid PermissionGroupId { get; set; }

    /// <summary>
    /// 权限组
    /// </summary>
    public virtual PermissionGroupOutput PermissionGroup { get; set; }
}
