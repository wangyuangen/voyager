using DotNetCore.CAP.Filter;
using Microsoft.Extensions.Logging;

namespace YK.EventBus.Cap;

/// <summary>
/// https://cap.dotnetcore.xyz/user-guide/zh/cap/filter/
/// </summary>
public sealed class DefaultCapFilter : SubscribeFilter
{
    private readonly ILogger<DefaultCapFilter> _logger;

    public DefaultCapFilter(ILogger<DefaultCapFilter> logger)
    {
        _logger = logger;
    }

    public override Task OnSubscribeExceptionAsync(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        return base.OnSubscribeExceptionAsync(context);
    }

    public override Task OnSubscribeExecutedAsync(ExecutedContext context)
    {
        return base.OnSubscribeExecutedAsync(context);
    }

    public override Task OnSubscribeExecutingAsync(ExecutingContext context)
    {
        return base.OnSubscribeExecutingAsync(context);
    }
}
