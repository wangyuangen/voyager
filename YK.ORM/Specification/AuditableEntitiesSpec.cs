using System.Linq.Expressions;
using YK.ORM.Contract;

namespace YK.ORM.Specification;

public class AuditableEntitiesBaseFilterSpec<T> : EntitiesBaseFilterSpec<T>
    where T : AuditableEntity
{
    public AuditableEntitiesBaseFilterSpec(BaseFilter filter, Expression<Func<T, bool>>? expression = null) 
        : base(filter,expression) => 
        Query.OrderByDescending(x => x.CreatedOn);
}

public class AuditableEntitiesBaseFilterSpec<T, TResult> : EntitiesBaseFilterSpec<T, TResult>
    where T : AuditableEntity
{
    public AuditableEntitiesBaseFilterSpec(BaseFilter filter, Expression<Func<T, bool>>? expression = null)
        : base(filter, expression) =>
        Query.OrderByDescending(x => x.CreatedOn);
}

public class AuditableEntitiesPaginationFilterSpec<T> : EntitiesPaginationFilterSpec<T>
    where T : AuditableEntity
{
    public AuditableEntitiesPaginationFilterSpec(PaginationFilter filter, Expression<Func<T, bool>>? expression = null)
        : base(filter, expression) =>
        Query.OrderByDescending(x => x.CreatedOn, !filter.HasOrderBy());
}

public class AuditableEntitiesPaginationFilterSpec<T, TResult> : EntitiesPaginationFilterSpec<T, TResult>
    where T : AuditableEntity
{
    public AuditableEntitiesPaginationFilterSpec(PaginationFilter filter, Expression<Func<T, bool>>? expression = null)
          : base(filter, expression) =>
        Query.OrderByDescending(x => x.CreatedOn, !filter.HasOrderBy());
}