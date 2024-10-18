using YK.Module.Core.Contracts;

namespace YK.Module.Core.Models;

/// <summary>
/// 文件存储信息简单输出
/// </summary>
public class FileStorageInfoSimpleOutput:BaseOutput
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string Extension { get; set; }

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
}
