using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YK.ORM.Abstractions;
using YK.ORM.Contract;
using YK.ORM.Persistence;
using YK.ORM.Specification;

namespace YK.ORM.Presistence;

public class ModuleRepositoryBase<T> : RepositoryBase<T>, IModuleRepositoryBase<T>
    where T : class, IEntity
{
    private readonly BaseDbContext _dbContext;

    private bool _ignoreDataPermissionFilte = false;

    private readonly IDataPermissionEvaluator _evaluator;

    public ModuleRepositoryBase(DbContext dbContext,IDataPermissionEvaluator evaluator) : base(dbContext)
    {
        _dbContext = (BaseDbContext)dbContext;
        _evaluator = evaluator;
    }

    public IReadRepository<T> SetGlobalFilterStatus(bool? ignoreDeleteFilter = false, bool? ignoreTenantFilter = false, bool? ignoreDataPermissionFilter = false)
    {
        if(ignoreDeleteFilter.HasValue && ignoreDeleteFilter!=_dbContext.IgnoreDeleteFilter)
            _dbContext.IgnoreDeleteFilter = ignoreDeleteFilter.Value;
        if(ignoreTenantFilter.HasValue && ignoreTenantFilter!=_dbContext.IgnoreTenantFilter)
            _dbContext.IgnoreTenantFilter = ignoreTenantFilter.Value;
        if(ignoreDataPermissionFilter.HasValue && ignoreDataPermissionFilter!=_ignoreDataPermissionFilte)
            _ignoreDataPermissionFilte=ignoreDataPermissionFilter.Value;
        return (IReadRepository<T>)this;
    }

    public IQueryable<T> NoDataPermissionQueryable()
        => _dbContext.Set<T>().AsQueryable();

    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
    {
        return _evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification, _ignoreDataPermissionFilte);
    }

    protected override IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
    {
        return _evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification, _ignoreDataPermissionFilte, evaluateCriteriaOnly);
    }

    #region simple repo

    public virtual Task<bool> SimpleAnyAsync(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSpec<T>(expression);
        return base.AnyAsync(spec, cancellationToken);
    }

    public virtual Task<TResult?> SimpleSingleAsync<TResult>(Expression<Func<T,bool>>? expression = null,CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSingleSpec<T, TResult>(expression);
        return base.SingleOrDefaultAsync(spec, cancellationToken);
    }

    public virtual Task<T?> SimpleSingleAsync(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSingleSpec<T>(expression);
        return base.SingleOrDefaultAsync(spec, cancellationToken);
    }

    public virtual Task<TResult?> SimpleFirstAsync<TResult>(Expression<Func<T,bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSpec<T, TResult>(expression);
        return base.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public virtual Task<T?> SimpleFirstAsync(Expression<Func<T,bool>>? expression = null,CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSpec<T>(expression);
        return base.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public virtual Task<int> SimpleCountAsync(Expression<Func<T,bool>>? expression = null,CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSpec<T>(expression);
        return base.CountAsync(spec, cancellationToken);
    }

    public virtual Task<List<TResult>> SimpleListAsync<TResult>(BaseFilter filter,Expression<Func<T,bool>>? expression=null,CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesBaseFilterSpec<T, TResult>(filter, expression);
        return base.ListAsync(spec, cancellationToken);
    }

    public virtual Task<List<TResult>> SimpleListAsync<TResult>(Expression<Func<T, bool>>? expression = null,CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSpec<T,TResult>(expression);
        return base.ListAsync(spec, cancellationToken);
    }

    public virtual async Task<PaginationResponse<TResult>> SimplePageAsync<TResult>(PaginationFilter filter, Expression<Func<T,bool>>? expression = null,CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesPaginationFilterSpec<T, TResult>(filter, expression);
        var list = await base.ListAsync(spec, cancellationToken);
        var count = await SimpleCountAsync(expression, cancellationToken);
        return new PaginationResponse<TResult>(list, count, filter.PageNumber, filter.PageSize);
    }

    public Task<int> DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        =>_dbContext.Set<T>().Where(expression).ExecuteDeleteAsync(cancellationToken);

    public Task<int> SoftDeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        var spec = new EntitiesExpressionSpec<T>(expression);
        var query = ApplySpecification(spec);
        _dbContext.Set<T>().RemoveRange(query);
        return _dbContext.SaveChangesAsync();
    }

    #endregion
}
