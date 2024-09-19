using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 角色菜单路由
/// </summary>
[DataSeederOrder(30)]
public class RoleMenuRoute : BaseEntity
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
    /// 菜单路由Id
    /// </summary>
    public Guid MenuRouteId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual MenuRouteInfo MenuRoute { get; set; }
}
