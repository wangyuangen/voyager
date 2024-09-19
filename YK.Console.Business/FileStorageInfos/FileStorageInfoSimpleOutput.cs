namespace YK.Console.Business.FileStorageInfos;

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
}
