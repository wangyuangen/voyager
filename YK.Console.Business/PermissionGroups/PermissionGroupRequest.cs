namespace YK.Console.Business.PermissionGroups;

/// <summary>
/// 创建权限组
/// </summary>
[UnitOfWork]
public class CreatePermissionGroupRequest:PermissionGroupInput,IRequest<Guid>
{
    /// <summary>
    /// 接口Id集合
    /// </summary>
    [Required]
    public List<Guid> ApiIds { get; set; }
}

/// <summary>
/// 编辑权限组
/// </summary>
[UnitOfWork]
public class UpdatePermissionGroupRequest:PermissionGroupInput,IRequest<Guid>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 接口Id集合
    /// </summary>
    [Required]
    public List<Guid> ApiIds { get; set; }
}

/// <summary>
/// 删除权限组
/// </summary>
public class DeletePermissionGroupRequest:IRequest<int>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    public DeletePermissionGroupRequest(Guid id)
    {
        Id= id;
    }
}

/// <summary>
/// 权限组列表查询接口一并返回
/// </summary>
public class PermissionGroupWithApiSearchRequest : BaseFilter, IRequest<List<PermissionGroupWithApiOutput>>
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}

/// <summary>
/// 获取可被授予的权限组命令
/// </summary>
public class SearchAuthPermissionGroupRequest : IRequest<List<PermissionGroupOutput>> { }


/// <summary>
/// 权限组列表查询
/// </summary>
public class PermissionGroupSearchRequest : BaseFilter, IRequest<List<PermissionGroupOutput>>
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}