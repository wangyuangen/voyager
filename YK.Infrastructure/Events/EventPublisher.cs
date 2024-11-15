﻿using Microsoft.Extensions.Logging;

namespace YK.Infrastructure.Events;

public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly IPublisher _mediator;

    public EventPublisher(ILogger<EventPublisher> logger, IPublisher mediator) =>
        (_logger, _mediator) = (logger, mediator);

    public Task PublishAsync(IDomainEvent @event)
    {
        _logger.LogInformation("Publishing Event : {event}", @event.GetType().Name);
        return _mediator.Publish(CreateEventNotification(@event));
    }

    private INotification CreateEventNotification(IDomainEvent @event) =>
        (INotification)Activator.CreateInstance(
            typeof(EventNotification<>).MakeGenericType(@event.GetType()), @event)!;
}
