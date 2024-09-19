using System.Text;

namespace YK.Infrastructure.Commons;

internal class HttpClientService(HttpClient _httpClient) : IHttpClientService
{
    public HttpRequestMessage BuildHttpRequestMessage(HttpRequestBuild requestBuild)
    {
        var httpRequest = new HttpRequestMessage
        {
            Method = requestBuild.Method,
            RequestUri = new Uri(requestBuild.RequestUrl),
            Content = BuildRequestContent(requestBuild.ContentBody, requestBuild.ContentEncoding, requestBuild.ContentMediaType)
        };
        if (!requestBuild.RequestHeaders.IsNullOrEmpty())
        {
            requestBuild.RequestHeaders?.ForEachItem(header =>
            {
                httpRequest.Headers.Add(header.Key, header.Value);
            });
        }
        if (!requestBuild.ContentHeaders.IsNullOrEmpty())
        {
            requestBuild.ContentHeaders?.ForEachItem(header =>
            {
                httpRequest.Content.Headers.Add(header.Key, header.Value);
            });
        }
        return httpRequest;
    }

    public StringContent BuildRequestContent(string content, Encoding? encoding, string mediaType = "application/json")
    {
        var endoding = encoding ?? Encoding.UTF8;
        return new StringContent(content, endoding, mediaType);
    }

    public async Task<HttpClientResult?> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        HttpClientResult? result = null;
        using (HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken))
        using (HttpContent responseContent = response.Content)
        {
            result = new HttpClientResult(response.Headers, responseContent, response.StatusCode);
        }
        return result;
    }
}
