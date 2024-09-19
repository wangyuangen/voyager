using Microsoft.AspNetCore.Http;
using OnceMi.AspNetCore.OSS;
using YK.Core.Commons.Models;
using YK.Core.Commons.Tools;

namespace YK.Infrastructure.FileStorage;

public class FileUploadService : IFileUploadService
{
    private readonly FIleUploadOptions _options;
    private readonly IOSSService _ossService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileUploadService(IOSSServiceFactory ossFactory, IHttpContextAccessor httpContextAccessor)
    {
        _options = AppCore.GetConfig<FIleUploadOptions>();

        if (_options.EnabledOssStorage)
        {
            _ossService = ossFactory.Create(_options.OSSProvider.ToString());
        }
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<bool> DeleteAsync(FileUploadInfo file)
    {
        string filePath = Path.Combine(file.FileDirectory, file.SaveFileName + file.Extension);
        if(file.Provider.HasValue && !file.BucketName.IsNullOrEmpty() && _options.EnabledOssStorage)
        {
            filePath = filePath.ToPath();
            return _ossService.RemoveObjectAsync(file.BucketName, filePath);
        }
        else
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath).ToPath();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }
        }
        return Task.FromResult(false);
    }

    public Task<FileUploadInfo> UploadAsync(IFormFile file,bool reName=false, CancellationToken cancellationToken = default)
    {
        string fileName = file.FileName;
        var extention = Path.GetExtension(fileName).ToLower();
        if (string.IsNullOrWhiteSpace(extention))
        {
            var contentTypeProvider = FileTool.GetFileExtensionContentTypeProvider();
            extention = contentTypeProvider.Mappings.FirstOrDefault(u => u.Value == file.ContentType).Key;
            if (extention == ".jpeg" || extention == ".jpe") extention = ".jpg";
            fileName = $"{fileName.TrimEnd('.')}{extention}";
        }
        return UploadAsync(file.OpenReadStream(), fileName,reName, cancellationToken);
    }

    public Task<FileUploadInfo> UploadAsync(string dataOfBase64, string fileName, bool reName = false, CancellationToken cancellationToken = default)
    {
        var streamData = new MemoryStream(Convert.FromBase64String(dataOfBase64));
        return UploadAsync(streamData, fileName,reName, cancellationToken);
    }

    public async Task<FileUploadInfo> UploadAsync(Stream stream, string fileName, bool reName = false, CancellationToken cancellationToken = default)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        
        if (!_options.AllowedExtension.IsNullOrEmpty() && _options.AllowedExtension.Length > 0 && !_options.AllowedExtension.Contains(extension))
            throw ResultOutput.Exception($"不允许上传的文件格式 {extension}");

        if (!_options.NotAllowedExtension.IsNullOrEmpty() && _options.NotAllowedExtension.Length > 0 && _options.NotAllowedExtension.Contains(extension))
            throw ResultOutput.Exception($"不允许上传的文件格式 {extension}");

        if (stream.Length > _options.MaxSize)
            throw ResultOutput.Exception($"文件大小不能超过 {new FileSize(_options.MaxSize)}");

        var fileSize = new FileSize(stream.Length);
        var storageInfo = new FileUploadInfo
        {
            FileGuid = Guid.NewGuid(),
            FileName = Path.GetFileNameWithoutExtension(fileName),
            Extension = extension,
            FileDirectory = Path.Combine(_options.RootDirectory, DateTime.Now.ToString(_options.DateFormatterDirectory)).ToPath(),
            Size = fileSize.Size,
            SizeFormat = fileSize.ToString(),
        };
        //文件重命名
        storageInfo.FileName = reName ? storageInfo.FileGuid.ToString() : storageInfo.FileName;

        storageInfo.SaveFileName = storageInfo.FileName.IsNullOrEmpty() ? storageInfo.FileGuid.ToString() : storageInfo.FileName;
        string filePath = Path.Combine(storageInfo.FileDirectory, storageInfo.SaveFileName + storageInfo.Extension).ToPath();

        string linkUrl = string.Empty;
        if (_options.EnabledOssStorage)
        {
            var ossOption = _options.OSSOptions.FirstOrDefault(x => x.Provider == _options.OSSProvider);
            if (ossOption != null)
            {
                linkUrl = $"{ossOption.Url}/{filePath}";
                if (ossOption.Url.IsNullOrEmpty())
                {
                    string url = ossOption.Provider switch
                    {
                        OSSProvider.Minio => $"{ossOption.Endpoint}/{ossOption.BucketName}",
                        OSSProvider.Aliyun => $"{ossOption.BucketName}.{ossOption.Endpoint}",
                        OSSProvider.QCloud => $"{ossOption.BucketName}-{ossOption.Endpoint}.cos.{ossOption.Region}.myqcloud.com",
                        OSSProvider.Qiniu => $"{ossOption.BucketName}.{ossOption.Region}.qiniucs.com",
                        OSSProvider.HuaweiCloud => $"{ossOption.BucketName}.{ossOption.Endpoint}",
                        _ => ""
                    };
                    var urlProtocol = ossOption.IsEnableHttps ? "https" : "http";
                    linkUrl = $"{urlProtocol}://{url}/{filePath}";
                }

                storageInfo.Provider = ossOption.Provider;
                storageInfo.BucketName = ossOption.BucketName;
                await _ossService.PutObjectAsync(ossOption.BucketName, filePath, stream, cancellationToken);
            }
        }
        else
        {
            linkUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host.Value}/{filePath}";

            string directory = Path.Combine(Directory.GetCurrentDirectory(), storageInfo.FileDirectory).ToPath();

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath).ToPath();

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await stream.CopyToAsync(fileStream, cancellationToken);
        }
        storageInfo.LinkUrl = linkUrl;

        return storageInfo;
    }
}
