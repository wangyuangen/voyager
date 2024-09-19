namespace YK.Core.Contract;

public abstract class DomainEvent : IDomainEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.Now ;
}
