using System.Security.Claims;
using YK.Core.Consts;

namespace YK.Core.Extensions;

public partial class Extensions
{
    public static string? GetUserId(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.UserId);

    public static string? GetUserStaffId(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.UserStaffId);

    public static string? GetPostId(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.PostId);

    public static string? GetUserAccount(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.UserAccount);

    public static string? GetRealName(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.RealName);

    public static string? GetNickName(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.NickName);

    public static string? GetTenantId(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.TenantId);

    public static string? GetTenantType(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.TenantType);

    public static string? GetUserStaffType(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.UserStaffType);

    public static string? GetOrgId(this ClaimsPrincipal principal)
        => principal.FindFirstValue(ClaimAttributes.OrgId);

    public static string? FindFirstValue(this IEnumerable<Claim> claims, string Type)
        => claims.FirstOrDefault(x => x.Type == Type)?.Value;

    public static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
            principal is null
                ? throw new ArgumentNullException(nameof(principal))
                : principal.FindFirst(claimType)?.Value;
}
