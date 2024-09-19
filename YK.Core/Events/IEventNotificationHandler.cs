using MediatR;
using YK.Core.Contract;

namespace YK.Core.Events;

public interface IEventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
where TEvent : IDomainEvent
{
}

public abstract class EventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
    where TEvent : IDomainEvent
{
    public Task Handle(EventNotification<TEvent> notification, CancellationToken cancellationToken) =>
        Handle(notification.Event, cancellationToken);

    public abstract Task Handle(TEvent @event, CancellationToken cancellationToken);
}
