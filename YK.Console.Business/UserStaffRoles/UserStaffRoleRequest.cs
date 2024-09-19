namespace YK.Console.Business.UserStaffRoles;

/// <summary>
/// 保存员工角色
/// </summary>
public class SaveUserStaffRoleRequest:IRequest<bool>
{
    /// <summary>
    /// 员工Id
    /// </summary>
    [Required]
    public Guid UserStaffId { get; set; }

    /// <summary>
    /// 角色Id集合
    /// </summary>
    [Required]
    public List<Guid> RoleIds { get; set; }
}
