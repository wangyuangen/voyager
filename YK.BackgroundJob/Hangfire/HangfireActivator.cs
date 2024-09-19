using Microsoft.Extensions.DependencyInjection;

namespace YK.BackgroundJob.Hangfire;

internal class HangfireActivator : JobActivator
{
    private readonly IServiceScopeFactory _scopeFactory;

    public HangfireActivator(IServiceScopeFactory scopeFactory) =>
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));

    public override JobActivatorScope BeginScope(PerformContext context) =>
        new Scope(context, _scopeFactory.CreateScope());

    private class Scope : JobActivatorScope, IServiceProvider
    {
        private readonly PerformContext _context;
        private readonly IServiceScope _scope;

        public Scope(PerformContext context, IServiceScope scope)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));

            ReceiveParameters();
        }

        private void ReceiveParameters()
        {
            var currentUserInitialzer = AppCore.GetRequiredService<ICurrentUserInitializer>();
            //由于后台job和http请求生命周期不同导致获取不到CurrentUser,于是创建job时传递租户和用户id至job
            //这里接收租户和用户Id参数写入CurrentUser
            var tenantId = _context.GetJobParameter<string>(QuerystringKeys.TenantId);
            if (!string.IsNullOrEmpty(tenantId))
            {
                currentUserInitialzer.SetCurrentTenantId(tenantId);
            }

            string userId = _context.GetJobParameter<string>(QuerystringKeys.UserId);
            if (!string.IsNullOrEmpty(userId))
            {
                currentUserInitialzer.SetCurrentUserId(userId);
            }
        }

        public override object Resolve(Type type) =>
            ActivatorUtilities.GetServiceOrCreateInstance(this, type);

        object? IServiceProvider.GetService(Type serviceType) =>
            serviceType == typeof(PerformContext)
                ? _context
                : _scope.ServiceProvider.GetService(serviceType);
    }
}
