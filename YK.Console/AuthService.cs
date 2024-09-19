using Microsoft.AspNetCore.Authorization;
using YK.Console.Business.Auths;
using YK.Console.Business.MenuRoutes;
using YK.Console.Core.Consts;
using YK.Core.Attributes;
using YK.Core.Options;

namespace YK.Console;

/// <summary>
/// 身份认证服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup), Order(1)]
public class AuthService(ISender _sender) : IDynamicApi
{
    /// <summary>
    /// 账户登录
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<Token> AccountLoginAsync(AccountLoginRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 手机号登录
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<Token> MobileLoginAsync(MobileLoginRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 刷新token
    /// </summary>
    /// <param name="token"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<Token> RefreshTokenAsync([Required]string token, CancellationToken cancellationToken)
        => _sender.Send(new RefreshTokenRequest(token), cancellationToken);

    /// <summary>
    /// 切换身份时刷新token
    /// </summary>
    /// <param name="userStaffId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<Token> StaffRefreshTokenAsync([Required] Guid userStaffId, CancellationToken cancellationToken)
        => _sender.Send(new RefreshTokenWithUserStaffRequest(userStaffId), cancellationToken);

    /// <summary>
    /// 获取当前登录用户身份信息
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<CurrentUserStaffOutput> CurrentStaffInfoAsync(CancellationToken cancellationToken)
        => _sender.Send(new GetCurrentUserStaffInfoRequest(), cancellationToken);

    /// <summary>
    /// 获取当前登录用户菜单
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<List<MenuRouteOutput>> CurrentMenuRouteAsync(CancellationToken cancellationToken)
        => _sender.Send(new CurrentUserStaffMenuRoutesRequest(), cancellationToken);

    /// <summary>
    /// 是否启用滑块验证
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public bool EnableCaptcha() => AppCore.GetConfig<AppOptions>().EnableSlideCaptcha;
}
