using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 雇佣员工的附属部门
/// </summary>
[DataSeederOrder(30)]
public class UserStaffOrg:BaseEntity
{
    /// <summary>
    /// 所属职工Id
    /// </summary>
    public Guid UserStaffId { get; set; }
    
    /// <summary>
    /// 附属部门Id
    /// </summary>
    public Guid OrgId { get; set; }

    /// <summary>
    /// 附属部门
    /// </summary>
    public virtual OrganizeInfo Org { get; set; }
}
