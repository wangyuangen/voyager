using YK.Console.Core.Enums;

namespace YK.Console.Business.RegionInfos;

/// <summary>
/// 创建行政区域命令
/// </summary>
public class CreateRegionInfoRequest : RegionInfoInput, IRequest<Guid> { }

/// <summary>
/// 编辑行政区域命令
/// </summary>
public class UpdateRegionInfoRequest:RegionInfoInput, IRequest<Guid> 
{
    /// <summary>
    /// 区域Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 删除行政区域命令
/// </summary>
public class DeleteRegionInfoRequest:IRequest<int>
{
    /// <summary>
    /// 区域Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 行政区域分页查询
/// </summary>
public class RegionInfoPageRequest:PaginationFilter,IRequest<PaginationResponse<RegionInfoPageOutput>>
{
    /// <summary>
    /// 上级Id
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public RegionLevel? Level { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    public bool? Hot { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}

/// <summary>
/// 行政区域列表查询
/// </summary>
public class RegionInfoSearchRequest:IRequest<List<RegionInfoSimpleOuput>>
{
    /// <summary>
    /// 上级Id
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    public bool? Hot { get; set; }

    /// <summary>
    /// 启用
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}

/// <summary>
/// 行政区域启用状态变更
/// </summary>
public class RegionInfoEnabledChangeRequest:IRequest<Guid>
{
    /// <summary>
    /// 区域Id
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
/// 行政区域热门设置
/// </summary>
public class RegionInfoHotChangeRequest:IRequest<Guid>
{
    /// <summary>
    /// 区域Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    [Required]
    public bool Hot { get; set; }
}