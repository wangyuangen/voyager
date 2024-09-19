namespace YK.ORM.Abstractions;

/// <summary>
/// 只读仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IReadRepository<T> : IReadRepositoryBase<T>, IModuleRepositoryBase<T>
    where T : class,IEntity
{

}
