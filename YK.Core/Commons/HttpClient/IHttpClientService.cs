using System.Text;
using YK.Core.Contract;

namespace YK.Core.Commons;

public interface IHttpClientService : ITransientService
{
    /// <summary>
    /// 发送Http请求
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<HttpClientResult?> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);

    /// <summary>
    /// 构建请求
    /// </summary>
    /// <param name="requestBuild"></param>
    /// <returns></returns>
    HttpRequestMessage BuildHttpRequestMessage(HttpRequestBuild requestBuild);
    
    /// <summary>
    /// 构建请求内容
    /// </summary>
    /// <param name="content"></param>
    /// <param name="encoding"></param>
    /// <param name="mediaType"></param>
    /// <returns></returns>
    StringContent BuildRequestContent(string content, Encoding? encoding, string mediaType = "application/json");
}
