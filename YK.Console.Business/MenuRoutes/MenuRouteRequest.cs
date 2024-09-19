namespace YK.Console.Business.MenuRoutes;

/// <summary>
/// 创建菜单命令
/// </summary>
public class CreateMenuRouteRequest : MenuRouteInput, IRequest<Guid> { }

/// <summary>
/// 编辑菜单命令
/// </summary>
public class UpdateMenuRouteRequest:MenuRouteInput, IRequest<Guid> 
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 删除菜单命令
/// </summary>
public class DeleteMenuRouteRequest : IRequest<int>
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    public DeleteMenuRouteRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 菜单列表查询
/// </summary>
public class MenuRouteSearchRequest :BaseFilter, IRequest<List<MenuRouteOutput>>
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}


/// <summary>
/// 获取可被授予的菜单命令
/// </summary>
public class SearchAuthMenuRoutesRequest : IRequest<List<MenuRouteSimpleOutput>> { }