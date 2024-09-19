namespace YK.Console.Business.ApiInfos;

/// <summary>
/// 从swagger文档同步接口
/// </summary>
public class SyncApiFromSwaggerRequest : IRequest<bool> { }

/// <summary>
/// 接口启用状态变更
/// </summary>
public class ApiInfoEnabledStatusChangeRequest : IRequest<Guid>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }
}

/// <summary>
/// 接口列表查询
/// </summary>
public class ApiInfoSearchRequest : BaseFilter, IRequest<List<ApiInfoOutput>> 
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}