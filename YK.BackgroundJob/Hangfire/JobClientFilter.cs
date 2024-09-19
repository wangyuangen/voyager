using Hangfire.Client;
using Hangfire.Logging;
using Microsoft.AspNetCore.Http;

namespace YK.BackgroundJob.Hangfire;

internal class JobClientFilter : IClientFilter
{
    private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

    public void OnCreating(CreatingContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        Logger.InfoFormat("Set TenantId and UserId parameters to job {0}.{1}...", context.Job.Method.ReflectedType?.FullName, context.Job.Method.Name);

        //var httpContext = AppCore.GetRequiredService<IHttpContextAccessor>().HttpContext;
        //_ = httpContext ?? throw new InvalidOperationException("Can't create a TenantJob without HttpContext.");

        //由于后台job和http请求生命周期不同导致获取不到CurrentUser,于是创建job时传递租户和用户id至job
        var currentUser = AppCore.GetRequiredService<ICurrentUser>();
        context.SetJobParameter(QuerystringKeys.TenantId, currentUser.TenantId);
        context.SetJobParameter(QuerystringKeys.UserId, currentUser.UserId);
    }

    public void OnCreated(CreatedContext context) =>
        Logger.InfoFormat(
            "Job created with parameters {0}",
            context.Parameters.Select(x => x.Key + "=" + x.Value).Aggregate((s1, s2) => s1 + ";" + s2));
}
