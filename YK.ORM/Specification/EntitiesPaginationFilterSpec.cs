using System.Linq.Expressions;
using YK.ORM.Contract;

namespace YK.ORM.Specification;

public class EntitiesPaginationFilterSpec<T, TResult> : EntitiesBaseFilterSpec<T, TResult>
    where T:class ,IEntity
{
    public EntitiesPaginationFilterSpec(PaginationFilter filter,Expression<Func<T,bool>>? expression=null)
          : base(filter,expression) =>
          Query.PaginateBy(filter);
}

public class EntitiesPaginationFilterSpec<T> : EntitiesBaseFilterSpec<T>
    where T : class, IEntity
{
    public EntitiesPaginationFilterSpec(PaginationFilter filter,Expression<Func<T,bool>>? expression=null)
    : base(filter, expression) =>
          Query.PaginateBy(filter);
}