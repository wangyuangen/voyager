namespace YK.Console.Business.Roles;

/// <summary>
/// 创建角色
/// </summary>
public class CreateRoleInfoRequest : RoleInfoInput, IRequest<Guid> { }

/// <summary>
/// 编辑角色
/// </summary>
public class UpdateRoleInfoRequest:RoleInfoInput,IRequest<Guid> 
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 获取角色授权信息
/// </summary>
public class GetRoleAuthInfoRequest:IRequest<RoleAuthInfoOutput>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    public GetRoleAuthInfoRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 角色授权变更
/// </summary>
public class RoleInfoAuthChangeRequest : IRequest<Guid>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 菜单Id集合
    /// </summary>
    [Required]
    public List<Guid> MenuRouteIds { get; set; }

    /// <summary>
    /// 权限组Id集合
    /// </summary>
    [Required]
    public List<Guid> PermissionGroupIds { get; set; }
}

/// <summary>
/// 删除角色
/// </summary>
public class DeleteRoleInfoRequest:IRequest<int>
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    public DeleteRoleInfoRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 角色列表查询
/// </summary>
public class RoleInfoSearchRequest:BaseFilter,IRequest<List<RoleInfoOutput>> 
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}

/// <summary>
/// 角色分页查询
/// </summary>
public class RoleInfoPageRequest : PaginationFilter, IRequest<PaginationResponse<RoleInfoOutput>> { }