using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 套餐菜单路由
/// </summary>
[DataSeederOrder(30)]
public class PackageMenuRoute: BaseEntity
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
    /// 菜单路由Id
    /// </summary>
    public Guid MenuRouteId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public virtual MenuRouteInfo MenuRoute { get; set; }
}
