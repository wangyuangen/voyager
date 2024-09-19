using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace YK.ORM.Persistence;

/// <summary>
/// 审计拦截器
/// </summary>
/// <param name="currentUser"></param>
public class AuditableEntityInterceptor(ICurrentUser currentUser) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:

                    //新增
                    if (entry.Entity is IAuditableEntity addedEntity)
                    {
                        addedEntity.CreatedBy = addedEntity.CreatedBy.IsNullOrEmpty()
                            ? currentUser.UserId
                            : addedEntity.CreatedBy;

                        addedEntity.CreatedOrgBy = addedEntity.CreatedOrgBy.IsNullOrEmpty()
                            ? currentUser.OrgId
                            : addedEntity.CreatedOrgBy;

                        addedEntity.CreatedUserStaffBy = addedEntity.CreatedUserStaffBy.IsNullOrEmpty()
                            ? currentUser.UserStaffId
                            : addedEntity.CreatedUserStaffBy;

                        addedEntity.CreatedUserName = addedEntity.CreatedUserName.IsNullOrEmpty()
                            ? currentUser.RealName ?? currentUser.NickName
                            : addedEntity.CreatedUserName;
                    }

                    //租户
                    if (entry.Entity is ITenant tenant)
                    {
                        tenant.TenantId = tenant.TenantId.IsNullOrEmpty()
                            ? currentUser.TenantId ?? Guid.Empty
                            : tenant.TenantId;
                    }

                    break;

                case EntityState.Modified:

                    if (entry.Entity is IAuditableEntity modifiedEntity)
                    {
                        //编辑信息
                        modifiedEntity.LastModifiedOn = DateTime.Now;
                        modifiedEntity.LastModifiedBy = currentUser.UserId;

                        modifiedEntity.ModifiedUserName = modifiedEntity.ModifiedUserName.IsNullOrEmpty()
                            ? currentUser.RealName ?? currentUser.NickName
                            : modifiedEntity.ModifiedUserName;
                    }

                    break;

                case EntityState.Deleted:

                    if (entry.Entity is ISoftDelete softDelete)
                    {
                        //软删除
                        softDelete.DeletedBy = currentUser.UserId;
                        softDelete.DeletedOn = DateTime.Now;
                        entry.State = EntityState.Modified;
                    }

                    break;
            }
        }
        context.ChangeTracker.DetectChanges();
    }
}
