using MediatR;
using YK.Core.Contract;

namespace YK.Core.Events;

public class EventNotification<TEvent> : INotification
where TEvent : IDomainEvent
{
    public EventNotification(TEvent @event) => Event = @event;

    public TEvent Event { get; }
}
