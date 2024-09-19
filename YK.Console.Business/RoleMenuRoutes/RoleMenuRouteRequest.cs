namespace YK.Console.Business.RoleMenuRoutes;

/// <summary>
/// 保存角色菜单
/// </summary>
public class SaveRoleMenuRouteRequest:IRequest<bool>
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [Required]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 菜单Id集合
    /// </summary>
    [Required]
    public List<Guid> MenuRouteIds { get; set; }
}
