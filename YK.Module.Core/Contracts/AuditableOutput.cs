using YK.Core.Contract;

namespace YK.Module.Core.Contracts;

public abstract class AuditableOutput:BaseOutput, IAuditableEntity
{
    public Guid CreatedBy { get; set; }
    public Guid? CreatedOrgBy { get; set; }
    public Guid? CreatedUserStaffBy { get; set; }
    public string? CreatedUserName { get; set; }
    public string? ModifiedUserName { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}
