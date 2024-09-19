using System.Text;

namespace YK.Core.Commons;

public class HttpRequestBuild
{
    public string RequestUrl { get; set; }
    public string? ContentBody { get; set; }
    public HttpMethod Method { get; set; }
    public Encoding? ContentEncoding { get; set; }
    public string ContentMediaType { get; set; }
    public Dictionary<string, string>? RequestHeaders { get; set; }
    public Dictionary<string, string>? ContentHeaders { get; set; }
    public HttpRequestBuild(string requestUrl, HttpMethod method, string? contentBody = null, Dictionary<string, string>?
        requestHeaders = null, Dictionary<string, string>? contentHeaders = null, Encoding? contentEncoding = null,
       string contentMediaType = "application/json")
    {
        RequestUrl = requestUrl;
        ContentBody = contentBody;
        Method = method;
        RequestHeaders = requestHeaders;
        ContentHeaders = contentHeaders;
        ContentEncoding = contentEncoding;
        ContentMediaType = contentMediaType;
    }
}
