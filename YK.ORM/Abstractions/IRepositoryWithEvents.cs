namespace YK.ORM.Abstractions;

/// <summary>
/// 领域事件仓储(建议只在需要发布领域事件时的增删改时使用)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryWithEvents<T> : IRepositoryBase<T>
  where T : class, IEntity
{
    
}
