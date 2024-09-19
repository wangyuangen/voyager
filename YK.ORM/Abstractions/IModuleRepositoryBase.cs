using System.Linq.Expressions;
using YK.ORM.Contract;

namespace YK.ORM.Abstractions;

public interface IModuleRepositoryBase<T>
    where T : class, IEntity
{
    /// <summary>
    /// 不受数权影响的Queryable查询
    /// </summary>
    /// <returns></returns>
    IQueryable<T> NoDataPermissionQueryable();

    /// <summary>
    /// 改变全局过滤与数权过滤状态
    /// </summary>
    /// <param name="ignoreDeleteFilter"></param>
    /// <param name="ignoreTenantFilter"></param>
    /// <param name="ignoreDataPermissionFilter"></param>
    /// <returns></returns>
    IReadRepository<T> SetGlobalFilterStatus(bool? ignoreDeleteFilter = false, bool? ignoreTenantFilter = false, bool? ignoreDataPermissionFilter = false);

    Task<bool> SimpleAnyAsync(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<TResult?> SimpleSingleAsync<TResult>(Expression<Func<T, bool>>? expression =null, CancellationToken cancellationToken = default);

    Task<T?> SimpleSingleAsync(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<TResult?> SimpleFirstAsync<TResult>(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<T?> SimpleFirstAsync(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<int> SimpleCountAsync(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<List<TResult>> SimpleListAsync<TResult>(BaseFilter filter, Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<List<TResult>> SimpleListAsync<TResult>(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<PaginationResponse<TResult>> SimplePageAsync<TResult>(PaginationFilter filter, Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);

    Task<int> DeleteAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}
