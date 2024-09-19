namespace YK.ORM.Abstractions;

/// <summary>
/// 仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> : IRepositoryBase<T>, IModuleRepositoryBase<T>
    where T : class, IEntity
{
}
