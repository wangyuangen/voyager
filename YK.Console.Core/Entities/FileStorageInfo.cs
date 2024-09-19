using OnceMi.AspNetCore.OSS;

namespace YK.Console.Core.Entities;

/// <summary>
/// 文件
/// </summary>
public class FileStorageInfo:AuditableEntity
{
    /// <summary>
    /// OSS供应商
    /// </summary>
    public OSSProvider? Provider { get; set; }

    /// <summary>
    /// 存储桶名称
    /// </summary>
    public string? BucketName { get; set; }

    /// <summary>
    /// 文件目录
    /// </summary>
    public string FileDirectory { get; set; }

    /// <summary>
    /// 文件Guid
    /// </summary>
    public Guid FileGuid { get; set; }

    /// <summary>
    /// 保存文件名
    /// </summary>
    public string SaveFileName { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string Extension { get; set; }

    /// <summary>
    /// 文件字节长度
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 文件大小格式化
    /// </summary>
    public string SizeFormat { get; set; }

    /// <summary>
    /// 链接地址
    /// </summary>
    public string LinkUrl { get; set; }

    /// <summary>
    /// 所属业务对象主键Id
    /// </summary>
    public Guid? BizId { get; set; }

    /// <summary>
    /// 所属业务对象名称
    /// </summary>
    public string? BizName { get; set; }

    public FileStorageInfo Update(OSSProvider? provider = null, string? bucketName=null, string? fileDirectory=null, Guid? fileGuid=null, string? saveFileName = null,
        string? fileName = null, string? extension = null, long? size = null, string? sizeFormat = null, string? linkUrl = null, Guid? bizId = null, string? bizName = null)
    {
       if(provider.HasValue && Provider!=provider)
            Provider = provider.Value;
       if(bucketName is not null && BucketName?.Equals(bucketName) is not true)
            BucketName = bucketName;
       if(fileDirectory is not null && FileDirectory?.Equals(fileDirectory) is not true)
            FileDirectory = fileDirectory;
        if (fileGuid.HasValue && FileGuid != fileGuid)
            FileGuid = fileGuid.Value;
        if(saveFileName is not null && SaveFileName?.Equals(saveFileName) is not true)
            SaveFileName = saveFileName;
        if (fileName is not null && FileName?.Equals(fileName) is not true)
            FileName = fileName;
        if(extension is not null && Extension?.Equals(extension) is not true)
            Extension = extension;
        if (size.HasValue && Size != size)
            Size = size.Value;
        if (sizeFormat is not null && SizeFormat?.Equals(sizeFormat) is not true)
            SizeFormat = sizeFormat;
        if(linkUrl is not null && LinkUrl?.Equals(linkUrl) is not true)
            LinkUrl = linkUrl;
        if(bizId.HasValue && BizId!=bizId)
            BizId = bizId.Value;
        if (bizName is not null && BizName?.Equals(bizName) is not true)
            BizName = bizName;

        return this;
    }



    /// <summary>
    /// 为所属业务对象名称
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void SetBizName<T>()
        => BizName = typeof(T).FullName;
}
