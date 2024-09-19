namespace YK.ORM.Abstractions;

public interface IDataPermissionEvaluator : ISpecificationEvaluator
{
    IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification, bool ignoreDataPermissionFilte)
        where T : class;

    IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification, bool ignoreDataPermissionFilte, bool evaluateCriteriaOnly = false)
          where T : class;
}
