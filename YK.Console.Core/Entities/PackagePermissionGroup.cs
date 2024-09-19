using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 套餐权限组
/// </summary>
[DataSeederOrder(20)]
public class PackagePermissionGroup:BaseEntity
{
    /// <summary>
    /// 套餐Id
    /// </summary>
    public Guid PackageId { get; set; }

    /// <summary>
    /// 套餐
    /// </summary>
    public virtual PackageInfo Package { get; set; }

    /// <summary>
    /// 权限组Id
    /// </summary>
    public Guid PermissionGroupId { get; set; }

    /// <summary>
    /// 权限组
    /// </summary>
    public virtual PermissionGroup PermissionGroup { get; set; }
}
