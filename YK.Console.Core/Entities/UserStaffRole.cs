using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 雇佣员工的角色
/// </summary>
[DataSeederOrder(20)]
public class UserStaffRole:BaseEntity
{
    /// <summary>
    /// 所属职工Id
    /// </summary>
    public Guid UserStaffId { get; set; }
    
    /// <summary>
    /// 角色Id
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public virtual RoleInfo Role { get; set; }
}
