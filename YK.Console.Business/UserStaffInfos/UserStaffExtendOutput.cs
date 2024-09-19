namespace YK.Console.Business.UserStaffInfos;

/// <summary>
/// 员工表单信息
/// </summary>
public class UserStaffExtendOutput:BaseOutput
{
    /// <summary>
    /// 附属机构Id集合
    /// </summary>
    public List<Guid> OrgIds { get; set; }

    /// <summary>
    /// 角色Id集合
    /// </summary>
    public List<Guid> RoleIds { get; set; }
}
