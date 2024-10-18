using YK.Core.Enums;
using YK.Module.Core.Contracts;
using YK.Module.Core.Enums;

namespace YK.Module.Core.Models;

/// <summary>
/// 员工输出
/// </summary>
public class UserStaffOutput : AuditableOutput
{
    /// <summary>
    /// 所属用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 岗位Id
    /// </summary>
    public Guid PostId { get; set; }

    /// <summary>
    /// 岗位
    /// </summary>
    public virtual PostInfoOutput Post { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 工号
    /// </summary>
    public string JobNo { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? EntryDate { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 雇佣所属机构Id
    /// </summary>
    public Guid OrgId { get; set; }

    /// <summary>
    /// 雇佣所属机构
    /// </summary>
    public virtual OrganizeInfoOutput Org { get; set; }

    /// <summary>
    /// 是否为主管
    /// </summary>
    public bool IsManager { get; set; }

    /// <summary>
    /// 职工类型
    /// </summary>
    public UserStaffTypeEnum UserStaffType { get; set; }
}
