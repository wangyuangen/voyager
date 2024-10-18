using System.Linq.Expressions;
using YK.ORM.Contract;

namespace YK.ORM.Specification;

public class EntitiesBaseFilterSpec<T, TResult> : EntitiesExpressionSpec<T, TResult>
    where T : class, IEntity
{
    public EntitiesBaseFilterSpec(BaseFilter filter, Expression<Func<T, bool>>? expression = null)
        : base(expression)
        => Query.SearchBy(filter);
}

public class EntitiesBaseFilterSpec<T> : EntitiesExpressionSpec<T>
    where T : class, IEntity
{
    public EntitiesBaseFilterSpec(BaseFilter filter, Expression<Func<T, bool>>? expression = null)
         : base(expression)
        => Query.SearchBy(filter);
}

public class EntitiesBaseFilterSortSpec<T> : EntitiesSortExpressionSpec<T>
        where T : class, IEntity
{
    public EntitiesBaseFilterSortSpec(BaseFilter filter, Expression<Func<T, bool>>? expression, string[]? orderFields) : base(expression, orderFields)
         => Query.SearchBy(filter);
}

public class EntitiesBaseFilterSortSpec<T, TResult> : EntitiesSortExpressionSpec<T, TResult>
        where T : class, IEntity
{
    public EntitiesBaseFilterSortSpec(BaseFilter filter, Expression<Func<T, bool>>? expression, string[]? orderFields) : base(expression, orderFields)
        => Query.SearchBy(filter);
}
