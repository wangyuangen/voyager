using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 权限组
/// </summary>
[DataSeederOrder(10)]
public class PermissionGroup: BaseSoftDeleteEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 数权范围
    /// </summary>
    public DataPermissionScope Scope { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 权限组角色列表
    /// </summary>
    public virtual List<RolePermissionGroup> RolePermissionGroups { get; set; }

    /// <summary>
    /// 权限组套餐列表
    /// </summary>
    public virtual List<PackagePermissionGroup> PackagePermissionGroups { get; set; }

    /// <summary>
    /// 权限组接口列表
    /// </summary>
    public virtual List<PermissionGroupApi>  PermissionGroupApis{ get; set; }

    public PermissionGroup Update(string? name = null, string? remark = null,
        DataPermissionScope? scope = null, int? sort = null, EnabledStatusEnum? enabled = null,Guid? parentId = null)
    {
        if (name is not null && Name?.Equals(name) is not true)
            Name = name;
        if (remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if (scope.HasValue && Scope != scope)
            Scope = scope.Value;
        if (sort.HasValue && Sort != sort)
            Sort = sort.Value;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        if(parentId.HasValue && ParentId!=parentId)
            ParentId = parentId.Value;
        return this;
    }
}
