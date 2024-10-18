using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 角色信息
/// </summary>
[DataSeederOrder(10)]
public class RoleInfo : BaseTenantSoftDeleteEntity
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 角色员工列表
    /// </summary>
    public virtual List<UserStaffRole> UserStaffRoles { get; set; }

    /// <summary>
    /// 角色菜单列表
    /// </summary>
    public virtual List<RoleMenuRoute> RoleMenuRoutes { get; set; }

    /// <summary>
    /// 角色权限组列表
    /// </summary>
    public virtual List<RolePermissionGroup> RolePermissionGroups { get; set; }

    public RoleInfo Update(string? name = null, string? remark = null, int? sort=null, EnabledStatusEnum? enabled= null)
    {
        if (name is not null && Name?.Equals(name) is not true)
            Name = name;
        if(remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if(sort.HasValue && Sort!=sort)
            Sort = sort.Value;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        return this;
    }
}
