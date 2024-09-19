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