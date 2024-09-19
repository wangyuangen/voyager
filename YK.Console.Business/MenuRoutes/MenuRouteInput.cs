using YK.Console.Core.Enums;

namespace YK.Console.Business.MenuRoutes;

/// <summary>
/// 菜单输入
/// </summary>
public class MenuRouteInput
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
    /// 菜单路由类型
    /// </summary>
    [Required]
    public MenuRouteTypeEnum Type { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required,MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [Required,MaxLength(128)]
    public string RouteUrl { get; set; }

    /// <summary>
    /// 路由命名,菜单同对应视图
    /// </summary>
    [MaxLength(64)]
    public string? RouteName { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    [MaxLength(128)]
    public string? RedirectUrl { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [MaxLength(64)]
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
    [MaxLength(512)]
    public string? Link { get; set; }

    /// <summary>
    /// 是否内嵌窗口
    /// </summary>
    public bool IsIframe { get; set; } = false;

    /// <summary>
    /// 排序
    /// </summary>
    [Required]
    public int Sort { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }
}
