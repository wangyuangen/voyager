namespace YK.Console.Business.ViewInfos;

/// <summary>
/// 创建视图命令
/// </summary>
public class CreateViewInfoRequest : ViewInfoInput, IRequest<Guid> { }

/// <summary>
/// 编辑视图命令
/// </summary>
public class UpdateViewInfoRequest : ViewInfoInput, IRequest<Guid>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 视图列表查询
/// </summary>
public class ViewInfoSearchRequest : BaseFilter, IRequest<List<ViewInfoOutput>> 
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled{ get; set; }
}

/// <summary>
/// 删除视图命令
/// </summary>
public class DeleteViewInfoRequest : IRequest<int>
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    public DeleteViewInfoRequest(Guid id)
    {
        Id = id;
    }
}