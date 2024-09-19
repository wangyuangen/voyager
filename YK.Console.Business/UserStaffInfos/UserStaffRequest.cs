using YK.Console.Business.UserInfos;

namespace YK.Console.Business.UserStaffInfos;

/// <summary>
/// 获取员工扩展信息
/// </summary>
public class GetUserStaffExtendRequest:IRequest<UserStaffExtendOutput>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    public GetUserStaffExtendRequest(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// 创建员工
/// </summary>
public class CreateUserStaffRequest:UserStaffInput,IRequest<Guid>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Required]
    public UserInfoInput UserInfo { get; set; }

    /// <summary>
    /// 附属机构Id集合
    /// </summary>
    public List<Guid>? OrgIds { get; set; }

    /// <summary>
    /// 角色Id集合
    /// </summary>
    [Required]
    public List<Guid> RoleIds { get; set; }
}

/// <summary>
/// 编辑员工个人信息
/// </summary>
public class UpdatePersonalUserStaffReuqest :IRequest<Guid> 
{
    /// <summary>
    /// 真实姓名
    /// </summary>
    [Required, MaxLength(16)]
    public string RealName { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }
}

/// <summary>
/// 编辑员工
/// </summary>
public class UpdateUserStaffRequest: UserStaffInput,IRequest<Guid>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 附属机构Id集合
    /// </summary>
    public List<Guid>? OrgIds { get; set; }

    /// <summary>
    /// 角色Id集合
    /// </summary>
    [Required]
    public List<Guid> RoleIds { get; set; }
}

/// <summary>
/// 员工分页查询
/// </summary>
public class UserStaffPageRequest :PaginationFilter, IRequest<PaginationResponse<UserStaffPageOutput>>
{
    /// <summary>
    /// 机构Id
    /// </summary>
    public Guid? OrgId { get; set; }

}

/// <summary>
/// 删除员工
/// </summary>
[UnitOfWork]
public class DeleteUserStaffRequest:IRequest<int>
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    public DeleteUserStaffRequest(Guid id)
    {
        Id = id;
    }
}


/// <summary>
/// 获取当前用户的多重员工身份
/// </summary>
public class GetCurrentUserStaffRequest : IRequest<List<UserStaffOutput>> { }