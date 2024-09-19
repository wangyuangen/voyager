using System.Security.Claims;

namespace YK.Infrastructure.Authorize;

public partial class CurrentUser
{
    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }
        _user = user;
    }

    public void SetCurrentUserId(string userId)
    {
        if (_userId != Guid.Empty)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }
        if (!userId.IsNullOrEmpty())
        {
            _userId = userId.ToGuid();
        }
    }

    public void SetCurrentTenantId(string tenantId)
    {
        if (_tenantId != Guid.Empty)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }
        if (!tenantId.IsNullOrEmpty())
        {
            _tenantId = tenantId.ToGuid();
        }
    }
}
