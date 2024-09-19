using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace YK.ORM.Persistence;

/// <summary>
/// 
/// </summary>
public class BaseDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;

    private readonly IEventPublisher _publisher;

    /// <summary>
    /// 忽略软删除过滤器
    /// </summary>
    public bool IgnoreDeleteFilter { get; set; } = false;

    /// <summary>
    /// 忽略租户过滤器
    /// </summary>
    public bool IgnoreTenantFilter { get; set; } = false;

    public BaseDbContext(
        DbContextOptions options, 
        IEventPublisher publisher,
        ICurrentUser currentUser) : base(options)
    {
        _publisher = publisher;
        Database.AutoTransactionBehavior = AutoTransactionBehavior.WhenNeeded;
        _currentUser = currentUser;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await PublishDomainEventsAsync().ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <returns></returns>
    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .SelectMany(e =>
            {
                var domainEvents = e.DomainEvents.ToList();
                e.DomainEvents.Clear();
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.PublishAsync(domainEvent).ConfigureAwait(false);
        }
    }

    #region global filter

    protected virtual void AppendGlobalFilters(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes()
             .Where(e => e.BaseType is null && e.ClrType.GetInterface(typeof(IEntity).Name) is not null)
             .Select(e => e.ClrType);

        MethodInfo? configureFilters = typeof(BaseDbContext).GetMethod(
           nameof(ConfigureFilters),
           BindingFlags.Instance | BindingFlags.NonPublic
        );

        configureFilters.CheckNull();

        foreach (Type entityType in entityTypes)
        {
            // 注册筛选器
            configureFilters?
                .MakeGenericMethod(entityType)
                .Invoke(this, new object[] { modelBuilder, entityType });
        }
    }

    /// <summary>
    /// 全局过滤
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="builder"></param>
    /// <param name="entityType"></param>
    protected virtual void ConfigureFilters<TEntity>(ModelBuilder builder, Type entityType)
           where TEntity : class
    {
        Expression<Func<TEntity, bool>>? expression = null;

        if (typeof(ISoftDelete).IsAssignableFrom(entityType))
        {
            expression = e => IgnoreDeleteFilter || EF.Property<Guid?>(e, nameof(ISoftDelete.DeletedBy)) == null;
        }

        if (typeof(ITenant).IsAssignableFrom(entityType))
        {
            Expression<Func<TEntity, bool>> tenantExpression = e => IgnoreTenantFilter || EF.Property<Guid>(e, nameof(ITenant.TenantId)) == (_currentUser.TenantId ?? Guid.Empty);
            expression = expression == null ? tenantExpression : expression.AndAlso(tenantExpression);
        }

        if (expression.IsNull()) return;

        builder.Entity<TEntity>().HasQueryFilter(expression);
    }

    #endregion
}
