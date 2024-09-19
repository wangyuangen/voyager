using YK.CRM.Core.Consts;
using YK.CRM.Core.Entities;

namespace YK.CRM.Core.DbContext;

public sealed class CrmDbContext(DbContextOptions<CrmDbContext> options,
        IEventPublisher publisher,
        ICurrentUser currentUser) : BaseDbContext(options,publisher,currentUser)
{
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// 模型创建
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrmDbContext).Assembly);
        modelBuilder.HasDefaultSchema(CrmAppConsts.DbSchema);
        AppendGlobalFilters(modelBuilder);
    }
}
