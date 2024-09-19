using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using YK.Core.Enums;

namespace YK.Infrastructure.Authorize;

public partial class CurrentUser(IHttpContextAccessor _httpContext) : ICurrentUser,ICurrentUserInitializer
{
    private Guid _userId = Guid.Empty;

    private Guid _tenantId = Guid.Empty;

    private ClaimsPrincipal? _user = null;

    private ClaimsPrincipal? User 
    {
        get
        {
            if (_user == null)
            {
                return _httpContext.HttpContext?.User;
            }
            return _user;   
        }
    }

    private bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated is true;

    public Guid UserId =>
        IsAuthenticated
        ? User?.GetUserId()?.ToGuidOrNull() ?? Guid.Empty
        : _userId;

    public Guid? TenantId => 
        IsAuthenticated 
        ? User?.GetTenantId()?.ToGuidOrNull()
        : _tenantId;

    public Guid? UserStaffId => 
        IsAuthenticated 
        ? User?.GetUserStaffId()?.ToGuidOrNull()
        : Guid.Empty;

    public Guid? PostId => 
        IsAuthenticated 
        ? User?.GetPostId()?.ToGuidOrNull() 
        : Guid.Empty;

    public string UserAccount => 
        IsAuthenticated 
        ? User?.GetUserAccount() ?? string.Empty 
        : string.Empty;

    public string RealName => 
        IsAuthenticated 
        ? User?.GetRealName() ?? string.Empty 
        : string.Empty;

    public string NickName =>
        IsAuthenticated
        ? User?.GetNickName() ?? string.Empty
        : string.Empty;

    public IEnumerable<Claim>? UserClaims => User?.Claims;

    public TenantTypeEnum? TenantType =>
        IsAuthenticated
        ? User?.GetTenantType()?.To<TenantTypeEnum>()
        : null;

    public UserStaffTypeEnum? UserStaffType =>
        IsAuthenticated
        ? User?.GetUserStaffType()?.To<UserStaffTypeEnum>()
        : null;

    public Guid? OrgId =>
         IsAuthenticated
        ? User?.GetOrgId()?.ToGuidOrNull()
        : Guid.Empty;
}
