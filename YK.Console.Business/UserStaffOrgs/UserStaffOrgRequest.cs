namespace YK.Console.Business.UserStaffOrgs;

/// <summary>
/// 保存员工附属部门
/// </summary>
public class SaveUserStaffOrgRequest:IRequest<bool>
{
    /// <summary>
    /// 员工Id
    /// </summary>
    [Required]
    public Guid UserStaffId { get; set; }

    /// <summary>
    /// 机构Id集合
    /// </summary>
    [Required]
    public List<Guid> OrgIds { get; set; }
}


/// <summary>
/// 根据权限范围获取员工部门
/// </summary>
public class GetUserStaffOrgsByPermissionScopeRequest:IRequest<UserStaffPermissionScope?>
{
    /// <summary>
    /// 权限范围
    /// </summary>
    public DataPermissionScope PermissionScope { get; set; }

    public GetUserStaffOrgsByPermissionScopeRequest(DataPermissionScope permissionScope)
    {
        PermissionScope = permissionScope;
    }
}