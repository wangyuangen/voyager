using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 角色权限组
/// </summary>
[DataSeederOrder(20)]
public class RolePermissionGroup : BaseEntity
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public virtual RoleInfo Role { get; set; }

    /// <summary>
    /// 权限组Id
    /// </summary>
    public Guid PermissionGroupId { get; set; }

    /// <summary>
    /// 权限组
    /// </summary>
    public virtual PermissionGroup PermissionGroup { get; set; }
}
