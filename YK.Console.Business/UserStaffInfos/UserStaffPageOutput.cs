using YK.Console.Business.UserInfos;

namespace YK.Console.Business.UserStaffInfos;

/// <summary>
/// 员工分页输出
/// </summary>
public class UserStaffPageOutput:UserStaffOutput
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public virtual UserInfoOutput User { get; set; }
}