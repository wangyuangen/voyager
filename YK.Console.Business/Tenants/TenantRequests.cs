namespace YK.Console.Business.Tenants;

/// <summary>
/// 创建租户命令
/// </summary>
public class CreateTenantRequest : TenantInput, IRequest<Guid> 
{
    /// <summary>
    /// 套餐Id集合
    /// </summary>
    [Required]
    public List<Guid> PackageIds { get; set; }
}

/// <summary>
/// 编辑租户命令
/// </summary>
public class UpdateTenantRequest : TenantInput, IRequest<Guid>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 套餐Id集合
    /// </summary>
    [Required]
    public List<Guid> PackageIds { get; set; }
}

/// <summary>
/// 租户分页查询命令
/// </summary>
public class TenantPageRequest : PaginationFilter, IRequest<PaginationResponse<TenantOutput>> { }