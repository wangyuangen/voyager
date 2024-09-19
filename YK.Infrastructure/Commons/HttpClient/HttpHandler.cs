using Microsoft.Extensions.Logging;

namespace YK.Infrastructure.Commons;

public class HttpHandler : DelegatingHandler, ITransientService
{
    private readonly ILogger<HttpHandler> _logger;
    public HttpHandler(ILogger<HttpHandler> logger)
        => _logger = logger;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage? httpResponseMessage = null;
        try
        {
            httpResponseMessage = await base.SendAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Failed to run http query {RequestUri}", request.RequestUri);
        }
        return httpResponseMessage ?? new HttpResponseMessage();
    }
}
