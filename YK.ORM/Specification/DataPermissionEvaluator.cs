using Ardalis.Specification.EntityFrameworkCore;
using YK.Core.Attributes;
using YK.Core.Enums;
using YK.ORM.Abstractions;
using YK.ORM.Contract;

namespace YK.ORM.Specification;

public class DataPermissionEvaluator(IPermissionValidator _permissionValidator, ICurrentUser _currentUser)
    : SpecificationEvaluator, IDataPermissionEvaluator
{
    public IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification, bool ignoreDataPermissionFilte)
          where T : class
    {
        if (!ignoreDataPermissionFilte) DataPermissionFilter(specification.Query);
        return base.GetQuery(query, specification);
    }

    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification, bool ignoreDataPermissionFilte, bool evaluateCriteriaOnly = false)
          where T : class
    {
        if (!ignoreDataPermissionFilte) DataPermissionFilter(specification.Query);
        return base.GetQuery(query, specification, evaluateCriteriaOnly);
    }

    private void DataPermissionFilter<T>(ISpecificationBuilder<T> query)
    {
        var permissionScope = _permissionValidator.GetPermissionScopeAsync().Result;

        //所有
        if (permissionScope.PermissionScope == DataPermissionScope.All) return;

        if (permissionScope.PermissionScope == DataPermissionScope.CurrentUserStaff)
        {
            //所属当前员工
            var ownerStaffProperties = typeof(T).GetPropertiesByAttribures<OwnerUserStaffAttribute>();
            if (ownerStaffProperties.Any())
            {
                var filter = new AdvancedFilter
                {
                    Logic = Enums.FilterLogicEnum.Or
                };

                var filters = new List<AdvancedFilter>();

                ownerStaffProperties.ForEach(ownerStaffProperty =>
                {
                    filters.Add(new AdvancedFilter
                    {
                        Field = ownerStaffProperty.Name,
                        Operator = Enums.FilterOperatorEnum.EQ,
                        Value = _currentUser.UserStaffId ?? Guid.Empty
                    });
                });

                filter.Filters = filters;

                query.AdvancedFilter(filter);
            }
        }
        else
        {
            //所属部门权限
            var ownerOrgProperties = typeof(T).GetPropertiesByAttribures<OwnerOrganizeAttribute>();
            if (ownerOrgProperties.Any())
            {
                var filter = new AdvancedFilter
                {
                    Logic = Enums.FilterLogicEnum.Or
                };

                var filters = new List<AdvancedFilter>();

                var orgScope = permissionScope.OrgScope.ToList();
                ownerOrgProperties.ForEach(ownerOrgProperty =>
                {
                    orgScope.ForEach(org =>
                    {
                        filters.Add(new AdvancedFilter
                        {
                            Field = ownerOrgProperty.Name,
                            Operator = Enums.FilterOperatorEnum.EQ,
                            Value = org
                        });
                    });
                });

                filter.Filters = filters;

                query.AdvancedFilter(filter);
            }
        }

    }
}
