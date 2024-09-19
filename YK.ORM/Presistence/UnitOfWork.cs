using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using YK.ORM.Abstractions;

namespace YK.ORM.Presistence;

public abstract class UnitOfWork(DbContext _dbContext) : IUnitOfWork
{
    protected IDbContextTransaction? DbTransaction { get; set; }

    public bool IsStartingUow => _dbContext.Database.CurrentTransaction is not null;

    protected IDbContextTransaction GetDbContextTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        => _dbContext.Database.BeginTransaction(isolationLevel);


    public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_dbContext.Database.CurrentTransaction is not null)
            throw new ArgumentException($"UnitOfWork Error,{_dbContext.Database.CurrentTransaction}");
        else
            DbTransaction = GetDbContextTransaction(isolationLevel);
    }

    public void Commit()
    {
        if (DbTransaction is null)
            throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
        else
            DbTransaction.Commit();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (DbTransaction is null)
            throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
        else
            await DbTransaction.CommitAsync(cancellationToken);
    }

    public void Rollback()
    {
        if (DbTransaction is null)
            throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
        else
            DbTransaction.Rollback();
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (DbTransaction is null)
            throw new ArgumentNullException(nameof(DbTransaction), "IDbContextTransaction is null");
        else
            await DbTransaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (DbTransaction is not null)
            {
                DbTransaction.Dispose();
                DbTransaction = null;
            }
        }
    }
}
