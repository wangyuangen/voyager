using OnceMi.AspNetCore.OSS;

namespace YK.Core.Options;

public class FIleUploadOptions
{
    /// <summary>
    /// 上传根目录
    /// </summary>
    public string RootDirectory { get; set; }

    /// <summary>
    /// 日期目录格式化
    /// </summary>
    public string DateFormatterDirectory { get; set; } = "yyyy/MM/dd";

    /// <summary>
    /// 上传文件的最大限制
    /// </summary>
    public long MaxSize { get; set; } = 104857600;

    /// <summary>
    /// 允许上传的文件后缀名
    /// </summary>
    public string[] AllowedExtension { get; set; }

    /// <summary>
    /// 不允许上传的文件后缀名
    /// </summary>
    public string[] NotAllowedExtension { get; set; }

    /// <summary>
    /// 使用OSS存储
    /// </summary>
    public bool EnabledOssStorage { get; set; }

    /// <summary>
    /// OSS文件存储供应商
    /// </summary>
    public OSSProvider OSSProvider { get; set; }

    /// <summary>
    /// oss配置
    /// </summary>
    public List<OSSOptions> OSSOptions { get; set; }
}

/// <summary>
/// OSS配置
/// </summary>
public class OSSOptions
{
    /// <summary>
    /// 文件存储供应商
    /// </summary>
    public OSSProvider Provider { get; set; }

    /// <summary>
    /// Endpoint
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// AccessKey
    /// </summary>
    public string AccessKey { get; set; }

    /// <summary>
    /// SecretKey
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    /// 地区
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// 会话Token
    /// </summary>
    public string SessionToken { get; set; }

    /// <summary>
    /// 启用Https
    /// </summary>
    public bool IsEnableHttps { get; set; }

    /// <summary>
    /// 启用缓存
    /// </summary>
    public bool IsEnableCache { get; set; }

    /// <summary>
    /// 存储桶
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    /// 文件地址
    /// </summary>
    public string Url { get; set; }
}