using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace YK.Core.Contract;

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; set; } = default!;
    [NotMapped]
    public Collection<DomainEvent> DomainEvents { get; } = new Collection<DomainEvent>();
    public void QueueDomainEvent(DomainEvent @event)
    {
        if (!DomainEvents.Contains(@event))
            DomainEvents.Add(@event);
    }
}

/// <summary>
/// 基础实体
/// </summary>
public abstract class BaseEntity : BaseEntity<Guid>
{
    protected BaseEntity() => Id = Guid.NewGuid();
}


/// <summary>
/// 租户隔离的基础实体
/// </summary>
public abstract class BaseTenantEntity : BaseEntity, ITenant
{
    public Guid TenantId { get; set; }
}
