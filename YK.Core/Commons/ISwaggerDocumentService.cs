using YK.Core.Commons.Models;
using YK.Core.Contract;

namespace YK.Core.Commons;

/// <summary>
/// swagger文档服务
/// </summary>
public interface ISwaggerDocumentService:IScopedService
{
    /// <summary>
    /// 获取所有接口文档信息
    /// </summary>
    /// <returns></returns>
    Task<List<SwaggerApiInfoDto>> GetAllApiInfoAsync();
}
