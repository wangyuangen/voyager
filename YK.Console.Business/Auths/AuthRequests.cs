using YK.Console.Business.MenuRoutes;

namespace YK.Console.Business.Auths;

/// <summary>
/// 用户登录命令
/// </summary>
public class AccountLoginRequest : IRequest<Token>
{
    /// <summary>
    /// 账户
    /// </summary>
    [Required, MaxLength(16)]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required, MaxLength(16)]
    public string Password { get; set; }

    /// <summary>
    /// 验证Id
    /// </summary>
    public string? CaptchaId { get; set; }

    /// <summary>
    /// 滑动轨迹
    /// </summary>
    public SlideTrack? Track { get; set; }
}

/// <summary>
/// 手机号登录命令
/// </summary>
public class MobileLoginRequest : IRequest<Token>
{
    /// <summary>
    /// 手机号
    /// </summary>
    [Required, MaxLength(16)]
    public string Mobile { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// 验证码Id
    /// </summary>
    [Required]
    public string CodeId { get; set; }
}

/// <summary>
/// 切换身份时刷新token命令
/// </summary>
public class RefreshTokenWithUserStaffRequest : IRequest<Token>
{
    /// <summary>
    /// 员工Id
    /// </summary>
    public Guid UserStaffId { get; set; }

    public RefreshTokenWithUserStaffRequest(Guid userStaffId)
    {
        UserStaffId = userStaffId;
    }
}

/// <summary>
/// 刷新token命令
/// </summary>
public class RefreshTokenRequest : IRequest<Token>
{
    /// <summary>
    /// token
    /// </summary>
    public string Token { get; set; }

    public RefreshTokenRequest(string token)
    {
        Token = token;
    }
}


/// <summary>
/// 当前登录用户身份信息
/// </summary>
public class GetCurrentUserStaffInfoRequest : IRequest<CurrentUserStaffOutput> { }


/// <summary>
/// 当前登录用户的菜单
/// </summary>
public class CurrentUserStaffMenuRoutesRequest : IRequest<List<MenuRouteOutput>> { }