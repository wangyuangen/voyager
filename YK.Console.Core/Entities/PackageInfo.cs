using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 套餐
/// </summary>
[DataSeederOrder(10)]
public class PackageInfo: BaseSoftDeleteEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 租户套餐列表
    /// </summary>
    public virtual List<TenantPackage> TenantPackages { get; set; }

    /// <summary>
    /// 套餐菜单列表
    /// </summary>
    public virtual List<PackageMenuRoute> PackageMenuRoutes { get; set; }

    /// <summary>
    /// 套餐权限组列表
    /// </summary>
    public virtual List<PackagePermissionGroup> PackagePermissionGroups { get; set; }

    public PackageInfo Update(string? name = null, EnabledStatusEnum? enabled = null, string? remark = null, int? sort = null)
    {
        if(name is not null && Name?.Equals(name) is not true)
            Name = name;
        if(enabled.HasValue && Enabled!=enabled)
            Enabled = enabled.Value;
        if(remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if (sort.HasValue && Sort != sort)
            Sort = sort.Value;
        return this;
    }
}
