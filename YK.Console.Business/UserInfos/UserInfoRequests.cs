using Microsoft.AspNetCore.Http;

namespace YK.Console.Business.UserInfos;

/// <summary>
/// 分页查询用户命令
/// </summary>
public class UserInfoPageRequest : PaginationFilter, IRequest<PaginationResponse<UserInfoOutput>> { }

/// <summary>
/// 创建普通用户命令
/// </summary>
public class CreateUserInfoRequest : UserInfoInput, IRequest<Guid> { }

/// <summary>
/// 编辑个人用户信息命令
/// </summary>
public class UpdatePersonalUserInfoRequest:UserInfoInput,IRequest<Guid> 
{
    /// <summary>
    /// 员工真实姓名
    /// </summary>
    [Required, MaxLength(16)]
    public string RealName { get; set; }

    /// <summary>
    /// 员工个人简介
    /// </summary>
    [MaxLength(512)]
    public string? Remark { get; set; }
}

/// <summary>
/// 编辑用户命令
/// </summary>
public class UpdateUserInfoRequest : UserInfoInput, IRequest<Guid>
{
    /// <summary>
    /// 用户主键
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}

/// <summary>
/// 上传个人头像命令
/// </summary>
public class UploadPersonalAvatarRequest : IRequest<string>
{
    /// <summary>
    /// 头像文件
    /// </summary>
    [Required]
    public IFormFile AvatarFile { get; set; }
}

/// <summary>
/// 上传用户头像
/// </summary>
[UnitOfWork]
public class UploadAvatarRequest : IRequest<string>
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// 头像文件
    /// </summary>
    [Required]
    public IFormFile AvatarFile { get; set; }
}

/// <summary>
/// 变更个人密码
/// </summary>
public class ChangePersonalPasswordRequest:IRequest<Guid>
{
    /// <summary>
    /// 旧密码
    /// </summary>
    [Required, MaxLength(16)]
    public string OldPassword { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required, MaxLength(16)]
    public string Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    [Required, MaxLength(16)]
    public string ConfirmPassword { get; set; }
}

/// <summary>
/// 变更用户密码
/// </summary>
public class ChangePasswordRequest : IRequest<Guid>
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// 旧密码
    /// </summary>
    [Required, MaxLength(16)]
    public string OldPassword { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required, MaxLength(16)]
    public string Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    [Required,MaxLength(16)]
    public string ConfirmPassword { get; set; }
}