using YK.ORM.Abstractions;

namespace YK.ORM.Presistence;

public class EventAddingRepositoryDecorator<T>(IRepository<T> decorated) : IRepositoryWithEvents<T>
    where T : class, IEntity
{
    private readonly IRepository<T> _decorated = decorated;

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.DomainEvents.Add(EntityCreatedEvent.WithEntity(entity));
        return _decorated.AddAsync(entity, cancellationToken);
    }

    public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        entities.ForEachItem(entity =>
        {
            entity.DomainEvents.Add(EntityCreatedEvent.WithEntity(entity));
        });
        return _decorated.AddRangeAsync(entities, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.DomainEvents.Add(EntityUpdatedEvent.WithEntity(entity));
        return _decorated.UpdateAsync(entity, cancellationToken);
    }

    public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        entities.ForEachItem(entity =>
        {
            entity.DomainEvents.Add(EntityUpdatedEvent.WithEntity(entity));
        });
        return _decorated.UpdateRangeAsync(entities, cancellationToken);
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.DomainEvents.Add(EntityDeletedEvent.WithEntity(entity));
        return _decorated.DeleteAsync(entity, cancellationToken);
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        entities.ForEachItem(entity =>
        {
            entity.DomainEvents.Add(EntityDeletedEvent.WithEntity(entity));
        });
        return _decorated.DeleteRangeAsync(entities, cancellationToken);
    }

    public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        =>_decorated.AnyAsync(specification, cancellationToken);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        =>_decorated.AnyAsync(cancellationToken);

    public IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification)
        =>_decorated.AsAsyncEnumerable(specification);

    public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        =>_decorated.CountAsync(specification, cancellationToken);

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
        => _decorated.CountAsync(cancellationToken);

    public Task DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        =>_decorated.DeleteRangeAsync(specification, cancellationToken);

    public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        =>_decorated.FirstOrDefaultAsync(specification, cancellationToken);

    public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        => _decorated.FirstOrDefaultAsync(specification, cancellationToken);

    public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        =>_decorated.GetByIdAsync(id, cancellationToken);

    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead.")]
    public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => _decorated.GetBySpecAsync(specification, cancellationToken);

    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead.")]
    public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        => _decorated.GetBySpecAsync(specification, cancellationToken);

    public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        =>_decorated.ListAsync(cancellationToken);

    public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        =>_decorated.ListAsync(specification, cancellationToken);

    public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        => _decorated.ListAsync(specification, cancellationToken);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        =>_decorated.SaveChangesAsync(cancellationToken);

    public Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
        =>_decorated.SingleOrDefaultAsync(specification, cancellationToken);

    public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        =>_decorated.SingleOrDefaultAsync(specification, cancellationToken);
}
