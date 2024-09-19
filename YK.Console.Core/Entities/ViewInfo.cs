using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 视图
/// </summary>
[DataSeederOrder(10)]
public class ViewInfo:AuditableSoftDeleteEntity
{
    /// <summary>
    /// 视图路由命名,ts中声明
    /// </summary>
    public string RouteName { get; set; }

    /// <summary>
    /// 视图名称
    /// </summary>
    public string Name{ get; set; }

    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool IsKeepAlive { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 上级
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 视图菜单列表
    /// </summary>
    public virtual List<MenuRouteInfo> MenuRouteInfos { get; set; }

    public ViewInfo Update(string? routeName=null, string? name=null, string? path = null, string? remark = null, bool? isKeepAlive=null, 
        int? sort=null, EnabledStatusEnum? enabled=null, Guid? parentId = null)
    {
        if(routeName is not null && RouteName?.Equals(RouteName) is not true)
            RouteName = routeName;
        if(name is not null && Name?.Equals(name) is not true)
            Name = name;    
        if(path is not null && Path?.Equals(path) is not true)
            Path = path;
        if(remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if (isKeepAlive.HasValue && IsKeepAlive != isKeepAlive)
            IsKeepAlive = isKeepAlive.Value;
        if(sort.HasValue && Sort!=sort)
            Sort = sort.Value;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        if(parentId.HasValue && ParentId!=parentId)
            ParentId = parentId.Value;
        return this;
    }
}
