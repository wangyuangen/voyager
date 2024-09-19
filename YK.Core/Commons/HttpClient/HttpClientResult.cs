using System.Net;
using System.Net.Http.Headers;

namespace YK.Core.Commons;

public class HttpClientResult
{
    public HttpResponseHeaders Headers { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public HttpContent Content { get; set; }

    public HttpClientResult(HttpResponseHeaders headers, HttpContent content, HttpStatusCode statusCode)
    {
        Headers = headers;
        Content = content;
        StatusCode = statusCode;
    }
}
