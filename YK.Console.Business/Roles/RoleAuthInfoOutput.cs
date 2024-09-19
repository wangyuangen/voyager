namespace YK.Console.Business.Roles;

/// <summary>
/// 角色授权信息
/// </summary>
public class RoleAuthInfoOutput : BaseOutput
{
    /// <summary>
    /// 菜单Id集合
    /// </summary>
    public List<Guid> MenuRouteIds { get; set; }

    /// <summary>
    /// 权限组Id集合
    /// </summary>
    public List<Guid> PermissionGroupIds { get; set; }
}
