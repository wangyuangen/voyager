using System.Linq.Expressions;

namespace YK.ORM.Specification;

public class EntitiesExpressionSpec<T, TResult> : Specification<T, TResult>
    where T : class, IEntity
{
    public EntitiesExpressionSpec(Expression<Func<T, bool>>? expression)
        => Query.Where(expression ?? (x => false), !expression.IsNull());
}

public class EntitiesExpressionSpec<T> : Specification<T>
    where T : class, IEntity
{
    public EntitiesExpressionSpec(Expression<Func<T, bool>>? expression)
        => Query.Where(expression ?? (x => false), !expression.IsNull());
}

public class EntitiesExpressionSingleSpec<T, TResult> : Specification<T, TResult>, ISingleResultSpecification<T,TResult>
    where T : class, IEntity
{
    public EntitiesExpressionSingleSpec(Expression<Func<T, bool>>? expression)
        => Query.Where(expression ?? (x => false), !expression.IsNull());
}

public class EntitiesExpressionSingleSpec<T> : Specification<T>, ISingleResultSpecification<T>
    where T : class, IEntity
{
    public EntitiesExpressionSingleSpec(Expression<Func<T, bool>>? expression)
        => Query.Where(expression ?? (x => false), !expression.IsNull());
}

public class EntitiesSortExpressionSpec<T> : EntitiesExpressionSpec<T>
       where T : class, IEntity
{
    public EntitiesSortExpressionSpec(Expression<Func<T, bool>>? expression, string[]? orderFields) : base(expression)
        => Query.OrderBy(orderFields);
}

public class EntitiesSortExpressionSpec<T,TResult> : EntitiesExpressionSpec<T, TResult>
       where T : class, IEntity
{
    public EntitiesSortExpressionSpec(Expression<Func<T, bool>>? expression, string[]? orderFields) : base(expression)
        => Query.OrderBy(orderFields);
}