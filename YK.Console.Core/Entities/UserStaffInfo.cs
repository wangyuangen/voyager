using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 用户雇佣信息
/// </summary>
[DataSeederOrder(30)]
public class UserStaffInfo:AuditableTenantSoftDeleteEntity
{
    /// <summary>
    /// 所属用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 所属用户
    /// </summary>
    public virtual UserInfo User { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 岗位Id
    /// </summary>
    public Guid PostId { get; set; }

    /// <summary>
    /// 岗位
    /// </summary>
    public virtual PostInfo Post { get; set; }

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
    public virtual OrganizeInfo Org { get; set; }

    /// <summary>
    /// 是否为主管
    /// </summary>
    public bool IsManager { get; set; }

    /// <summary>
    /// 职工类型
    /// </summary>
    public UserStaffTypeEnum UserStaffType { get; set; }


    public UserStaffInfo Update(Guid? userId = null, string? realName = null, Guid? postId= null,string? jobNo= null, EnabledStatusEnum? enabled= null,
        DateTime? entryDate = null, string? remark = null, Guid? orgId = null, bool? isManager = null,UserStaffTypeEnum? userStaffType = null)
    {
        if(userId.HasValue && UserId!=userId)
            UserId = userId.Value;
        if(realName is not null && RealName?.Equals(realName) is not true)
            RealName = realName;
        if (postId.HasValue && PostId != postId)
            PostId = postId.Value;
        if(jobNo is not null && JobNo?.Equals(jobNo) is not true)
            JobNo = jobNo;
        if (enabled.HasValue && Enabled != enabled)
            Enabled = enabled.Value;
        if(entryDate.HasValue && EntryDate!=entryDate)
            EntryDate = entryDate.Value;
        if(remark is not null && Remark!= null)
            Remark = remark;
        if(orgId.HasValue && OrgId!=orgId)
            OrgId = orgId.Value;
        if(isManager.HasValue && IsManager!=isManager)
            IsManager = isManager.Value;
        if (userStaffType.HasValue && UserStaffType != userStaffType)
            UserStaffType = userStaffType.Value;
        return this;
    }
}
