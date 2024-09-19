﻿namespace YK.CRM.Core.DbContext;

public class CrmDbRepository<T> : ModuleRepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class,IEntity
{
    public CrmDbRepository(CrmDbContext dbContext,IDataPermissionEvaluator dataPermissionEvaluator) 
        : base(dbContext, dataPermissionEvaluator) { }

    // We override the default behavior when mapping to a dto.
    // We're using Mapster's ProjectToType here to immediately map the result from the database.
    // This is only done when no Selector is defined, so regular specifications with a selector also still work.
    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        specification.Selector is not null
            ? base.ApplySpecification(specification)
            : ApplySpecification(specification, false)
                .ProjectToType<TResult>();
}
