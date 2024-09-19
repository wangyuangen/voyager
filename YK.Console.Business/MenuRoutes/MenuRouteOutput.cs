using YK.Console.Business.ViewInfos;
using YK.Console.Core.Enums;

namespace YK.Console.Business.MenuRoutes;

public class MenuRouteSimpleOutput:BaseOutput
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 父级
    /// </summary>
    public Guid? ParentId { get; set; }
}

/// <summary>
/// 菜单输出
/// </summary>
public class MenuRouteOutput: AuditableOutput
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
    public virtual ViewInfoOutput? View { get; set; }

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
}
