using YK.Console.Core.Enums;
using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 菜单路由
/// </summary>
[DataSeederOrder(20)]
public class MenuRouteInfo: AuditableSoftDeleteEntity
{
    /// <summary>
    /// 父级
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 视图id
    /// </summary>
    public Guid? ViewId { get; set; }

    /// <summary>
    /// 视图
    /// </summary>
    public virtual ViewInfo? View { get; set; }

    /// <summary>
    /// 菜单路由类型
    /// </summary>
    public MenuRouteTypeEnum Type { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    public string RouteUrl { get; set; }

    /// <summary>
    /// 路由命名,菜单同对应视图
    /// </summary>
    public string? RouteName { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    public string? RedirectUrl { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 隐藏
    /// </summary>
    public bool Hidden { get; set; } = false;

    /// <summary>
    /// 展开分组
    /// </summary>
    public bool Opened { get; set; }

    /// <summary>
    /// 打开新窗口
    /// </summary>
    public bool NewWindow { get; set; } = false;

    /// <summary>
    /// 链接外显
    /// </summary>
    public bool External { get; set; } = false;

    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool IsKeepAlive { get; set; } = true;

    /// <summary>
    /// 是否固定
    /// </summary>
    public bool IsAffix { get; set; } = false;

    /// <summary>
    /// 内/外部链接
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// 是否内嵌窗口
    /// </summary>
    public bool IsIframe { get; set; } = false;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 菜单角色列表
    /// </summary>
    public virtual List<RoleMenuRoute> RoleMenuRoutes { get; set; }

    /// <summary>
    /// 菜单套餐列表
    /// </summary>
    public virtual List<PackageMenuRoute> PackageMenuRoutes { get; set; }


    public MenuRouteInfo Update(Guid? parentId=null, Guid? viewId= null,MenuRouteTypeEnum? type= null, string? name=null, string? routeUrl = null, 
        string? routeName = null, string? redirectUrl = null, string? icon = null, bool? hidden = null, bool? opened = null, bool? newWindow = null,
        bool? external = null,  bool? isKeepAlive = null,bool? isAffix = null, string? link = null, bool? isIframe = null, int? sort = null, string? remark = null,
        EnabledStatusEnum? enabled=null)
    {
        if(parentId.HasValue && ParentId!=parentId)
            ParentId = parentId.Value;
        if(viewId.HasValue && ViewId!=viewId)
            ViewId = viewId.Value;
        if(type.HasValue && Type!=type)
            Type = type.Value;
        if(name is not null && Name?.Equals(name) is not true)
            Name = name;
        if(routeUrl is not null && RouteUrl?.Equals(routeUrl) is not true)
            RouteUrl = routeUrl;
        if(routeName is not null && RouteName?.Equals(routeName) is not true)
            RouteName = routeName;
        if(redirectUrl is not null && RedirectUrl?.Equals(redirectUrl) is not true)
            RedirectUrl = redirectUrl;
        if(icon is not null && Icon?.Equals(icon) is not true)
            Icon = icon;
        if (hidden.HasValue && Hidden != hidden)
            Hidden = hidden.Value;
        if(opened.HasValue && Opened != opened)
            Opened = opened.Value;
        if(newWindow.HasValue && NewWindow!=newWindow)
            NewWindow = newWindow.Value;
        if(external.HasValue && External!=external)
            External = external.Value;
        if(isKeepAlive.HasValue && IsKeepAlive!=isKeepAlive)
            IsKeepAlive = isKeepAlive.Value;
        if(isAffix.HasValue && IsAffix!=isAffix)
            IsAffix = isAffix.Value;
        if(link is not null && Link?.Equals(link) is not true)
            Link = link;
        if(isIframe.HasValue && IsIframe!=isIframe)
            IsIframe = isIframe.Value;
        if (sort.HasValue && Sort != sort)
            Sort = sort.Value;
        if (remark is not null && Remark?.Equals(remark) is not true)
            Remark = remark;
        if(enabled.HasValue && Enabled !=enabled) 
            Enabled = enabled.Value;
        return this;
    }
}
