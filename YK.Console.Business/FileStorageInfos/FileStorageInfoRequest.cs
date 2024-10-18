using Microsoft.AspNetCore.Http;

namespace YK.Console.Business.FileStorageInfos;

/// <summary>
/// 批量上传文件命令
/// </summary>
public class BulkUploadFileStorageInfoRequest : IRequest<List<FileStorageInfoSimpleOutput>>
{
    /// <summary>
    /// 文件集合
    /// </summary>
    [Required]
    public IFormFileCollection Files { get; set; }

    /// <summary>
    /// 文件重命名
    /// </summary>
    public bool ReName { get; set; } = false;

    /// <summary>
    /// 业务实体主键
    /// </summary>
    public Guid? BizId { get; set; }

    /// <summary>
    /// 业务实体类型 fullName
    /// </summary>
    public string? BizName { get; set; }
}

/// <summary>
/// 上传文件命令
/// </summary>
public class UploadFileStorageInfoRequest:IRequest<FileStorageInfoSimpleOutput>
{
    /// <summary>
    /// 文件
    /// </summary>
    [Required]
    public IFormFile File { get; set; }

    /// <summary>
    /// 文件重命名
    /// </summary>
    public bool ReName { get; set; } = false;

    /// <summary>
    /// 业务实体主键
    /// </summary>
    public Guid? BizId { get; set; }

    /// <summary>
    /// 业务实体类型 fullName
    /// </summary>
    public string? BizName { get; set; }
}

//删除文件命令
public class DeleteFileStorageInfoRequest:IRequest<Guid>
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    public DeleteFileStorageInfoRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 文件与业务实体绑定
/// </summary>
public class FileStorageInfoBindBizRequest : IRequest<Guid>
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 业务Id
    /// </summary>
    [Required]
    public Guid BizId { get; set; }

    /// <summary>
    /// 业务实体类型fullName
    /// </summary>
    [Required]
    public string BizName { get; set; }
}

/// <summary>
/// 查询业务文件列表
/// </summary>
public class FileStorageInfoSearchByBizRequest:IRequest<List<FileStorageInfoSimpleOutput>>
{
    /// <summary>
    /// 业务实体主键
    /// </summary>
    [Required]
    public Guid BizId { get; set; }

    /// <summary>
    /// 业务实体类型fullName
    /// </summary>
    [Required]
    public string BizName { get; set; }
}

/// <summary>
/// 查询多个业务文件列表
/// </summary>
public class FileStorageInfoSearchByBizListRequest : IRequest<List<FileStorageInfoSimpleOutput>>
{
    /// <summary>
    /// 业务实体主键集合
    /// </summary>
    [Required]
    public List<Guid> BizIds { get; set; }

    /// <summary>
    /// 业务实体类型fullName
    /// </summary>
    [Required]
    public string BizName { get; set; }
}

/// <summary>
/// 获取单个文件
/// </summary>
public class GetFileStorageInfoRequest : IRequest<FileStorageInfoSimpleOutput>
{
    /// <summary>
    /// 文件Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    public GetFileStorageInfoRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 文件分页查询
/// </summary>
public class FileStorageInfoPageRequest:PaginationFilter,IRequest<PaginationResponse<FileStorageInfoOutput>> { }