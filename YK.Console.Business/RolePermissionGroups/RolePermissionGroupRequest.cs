namespace YK.Console.Business.RolePermissionGroups;

/// <summary>
/// 保存角色权限组
/// </summary>
public class SaveRolePermissionGroupRequest:IRequest<bool>
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [Required]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 权限组Id集合
    /// </summary>
    [Required]
    public List<Guid> PermissionGroupIds { get; set; }
}
