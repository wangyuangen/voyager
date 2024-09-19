namespace YK.Console.Business.UserStaffInfos;

/// <summary>
/// 员工信息
/// </summary>
public class UserStaffInput
{
    /// <summary>
    /// 岗位Id
    /// </summary>
    [Required]
    public Guid PostId { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    [Required,MaxLength(16)]
    public string RealName { get; set; }

    /// <summary>
    /// 工号
    /// </summary>
    [Required,MaxLength(32)]
    public string JobNo { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    [Required]
    public EnabledStatusEnum Enabled { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? EntryDate { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }

    /// <summary>
    /// 雇佣所属机构Id
    /// </summary>
    [Required]
    public Guid OrgId { get; set; }

    /// <summary>
    /// 是否为主管
    /// </summary>
    [Required]
    public bool IsManager { get; set; } = false;
}
