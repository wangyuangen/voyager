using System.Data;

namespace YK.ORM.Abstractions;

public interface IUnitOfWork: IDisposable
{
    bool IsStartingUow { get; }

    void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    void Rollback();

    void Commit();

    Task RollbackAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);
}
