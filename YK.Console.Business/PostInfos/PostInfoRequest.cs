namespace YK.Console.Business.PostInfos;

/// <summary>
/// 创建岗位命令
/// </summary>
public class CreatePostInfoRequest : PostInfoInput, IRequest<Guid> { }

/// <summary>
/// 编辑岗位命令
/// </summary>
public class UpdatePostInfoRequest : PostInfoInput, IRequest<Guid>
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 删除岗位命令
/// </summary>
public class DeletePostInfoRequest : IRequest<int>
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    public DeletePostInfoRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 岗位列表查询命令
/// </summary>
public class PostInfoSearchRequest : BaseFilter, IRequest<List<PostInfoOutput>> { }

/// <summary>
/// 岗位分页查询命令
/// </summary>
public class PostInfoPageSearchRequest : PaginationFilter, IRequest<PaginationResponse<PostInfoOutput>> { }