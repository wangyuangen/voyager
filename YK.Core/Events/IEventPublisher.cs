using YK.Core.Contract;

namespace YK.Core.Events;

public interface IEventPublisher : ITransientService
{
    /// <summary>
    /// 发布领域事件
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    Task PublishAsync(IDomainEvent @event);
}
