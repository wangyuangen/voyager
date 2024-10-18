namespace YK.Console.Business.DataDicts;

/// <summary>
/// 创建字典命令
/// </summary>
public class CreateDataDictRequest : DataDictInput, IRequest<Guid> { }

/// <summary>
/// 编辑字典
/// </summary>
public class UpdateDataDictRequest:DataDictInput, IRequest<Guid> 
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}


/// <summary>
/// 删除字典
/// </summary>
public class DeleteDataDictRequest:IRequest<Guid>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public DeleteDataDictRequest(Guid id)
    {
        this.Id = id; 
    }
}

/// <summary>
/// 字典列表查询
/// </summary>
public class DataDictSearchRequest : IRequest<List<DataDictOutput>> 
{
    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum? Enabled { get; set; }
}

/// <summary>
/// 字典分页查询
/// </summary>
public class DataDictPageRequest : PaginationFilter, IRequest<PaginationResponse<DataDictOutput>> 
{
    /// <summary>
    /// 字典父级编码
    /// </summary>
    [MaxLength(32)]
    public string? ParentCode { get; set; }
}