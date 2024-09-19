using YK.Console.Business.MenuRoutes;

namespace YK.Console.Business.PackageMenuRoutes;

public class PackageMenuRouteOutput:BaseOutput
{
    /// <summary>
    /// 套餐Id
    /// </summary>
    public Guid PackageId { get; set; }

    /// <summary>
    /// 菜单路由Id
    /// </summary>
    public Guid MenuRouteId { get; set; }

    /// <summary>
    /// 菜单路由
    /// </summary>
    public virtual MenuRouteSimpleOutput MenuRoute { get; set; }
}
