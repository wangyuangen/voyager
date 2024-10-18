using MassTransit;
using YK.Core.Attributes;

namespace YK.Core.Contract;

/// <summary>
/// 审计实体
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class AuditableEntity: BaseEntity, IAuditableEntity
{
    [OwnerOrganize]
    public Guid? CreatedOrgBy { get; set; }

    [OwnerUserStaff]
    public Guid? CreatedUserStaffBy { get; set; }

    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn{ get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }

    public string? CreatedUserName { get; set; }
    public string? ModifiedUserName { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTime.Now;
        Id = NewId.NextSequentialGuid();    //有序guid
    }
}

/// <summary>
/// 软删除的审计实体
/// </summary>
public abstract class AuditableSoftDeleteEntity : AuditableEntity, ISoftDelete
{
    public DateTime? DeletedOn { get ; set ; }
    public Guid? DeletedBy { get; set; }
}

/// <summary>
/// 租户隔离的审计实体
/// </summary>
public abstract class AuditableTenantEntity : AuditableEntity, ITenant
{
    public Guid TenantId { get; set;  }
}

/// <summary>
/// 租户隔离的软删除审计实体
/// </summary>
public abstract class AuditableTenantSoftDeleteEntity : AuditableEntity, ISoftDelete, ITenant
{
    public DateTime? DeletedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public Guid TenantId { get; set; }
}
