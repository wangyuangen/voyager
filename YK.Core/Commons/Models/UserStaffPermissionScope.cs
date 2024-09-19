using YK.Core.Enums;

namespace YK.Core.Commons.Models;

/// <summary>
/// 员工权限范围
/// </summary>
public class UserStaffPermissionScope
{
    /// <summary>
    ///  当前部门+下级部门 | 当前部门+附属部门 | 当前部门+下级部门+附属部门
    /// </summary>
    public IEnumerable<Guid> OrgScope { get; private set; }

    /// <summary>
    /// 权限范围
    /// </summary>
    public DataPermissionScope PermissionScope { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public UserStaffPermissionScope()
    {
        OrgScope = new List<Guid>();
        PermissionScope = DataPermissionScope.CurrentUserStaff;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="permissionScope"></param>
    public UserStaffPermissionScope(DataPermissionScope permissionScope)
    {
        PermissionScope = permissionScope;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orgScope"></param>
    /// <param name="permissionScope"></param>
    public UserStaffPermissionScope(IEnumerable<Guid> orgScope, DataPermissionScope permissionScope)
    {
        OrgScope = orgScope;
        PermissionScope = permissionScope;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orgScope"></param>
    /// <param name="permissionScope"></param>
    /// <returns></returns>
    public UserStaffPermissionScope Update(IEnumerable<Guid>? orgScope = null, DataPermissionScope? permissionScope = null)
    {
        if(orgScope!=null)
            OrgScope = orgScope;
        if (permissionScope.HasValue && permissionScope != PermissionScope)
            PermissionScope = permissionScope.Value;
        return this;
    }
}
