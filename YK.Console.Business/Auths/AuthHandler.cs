using Microsoft.EntityFrameworkCore;
using YK.Console.Business.MenuRoutes;
using YK.Console.Business.PermissionGroupApis;
using YK.Console.Business.UserStaffInfos;
using YK.Console.Business.ViewInfos;

namespace YK.Console.Business.Auths;

internal class MobileLoginHandler : IRequestHandler<MobileLoginRequest, Token>
{
    public Task<Token> Handle(MobileLoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

internal class AccountLoginHandler(IReadRepository<UserInfo> _userRepo, IReadRepository<UserStaffInfo> _userStaffRepo, ITokenBuilder _tokenBuilder) 
    : IRequestHandler<AccountLoginRequest, Token>
{
    public async Task<Token> Handle(AccountLoginRequest request, CancellationToken cancellationToken)
    {
        //身份校验
        var user = await _userRepo
            .SetGlobalFilterStatus(ignoreDataPermissionFilter:true)
            .SimpleFirstAsync(x => x.Account == request.Account, cancellationToken)
            ?? throw ResultOutput.Exception($"账户{request.Account}不存在");

        if (!user.VerifyPassword(request.Password)) throw ResultOutput.Exception("密码不正确");

        var staffCount = await _userStaffRepo.SetGlobalFilterStatus(ignoreTenantFilter:true,ignoreDataPermissionFilter:true)
            .SimpleCountAsync(x => x.UserId == user.Id 
                && x.Enabled == EnabledStatusEnum.Enabled 
                && x.Org.Enabled == EnabledStatusEnum.Enabled
                && x.Org.Tenant.Enabled == EnabledStatusEnum.Enabled, cancellationToken);

        if (staffCount == 0)
            throw ResultOutput.Exception("非机构员工或机构与员工被停用");

        return _tokenBuilder.BuildToken(new UserClaimInfo
        {
            NickName = user.NickName,
            UserAccount = user.Account,
            UserId = user.Id,
        }) ;
    }
}

internal class RefreshTokenWithUserStaffHandler(IReadRepository<UserStaffInfo> _repo, ITokenBuilder _tokenBuilder) : IRequestHandler<RefreshTokenWithUserStaffRequest, Token>
{
    public async Task<Token> Handle(RefreshTokenWithUserStaffRequest request, CancellationToken cancellationToken)
    {
        var userStaff = await _repo.SetGlobalFilterStatus(ignoreTenantFilter:true)
            .NoDataPermissionQueryable()
            .AsNoTracking()
            .Where(x => x.Id == request.UserStaffId)
            .Include(x => x.User)
            .Include(x => x.Org.Tenant)
            .FirstOrDefaultAsync();

        if (userStaff == null) 
            throw ResultOutput.Exception("员工不存在");

        if (userStaff.Enabled == EnabledStatusEnum.Disabled)
            throw ResultOutput.Exception("员工已禁用");

        if (userStaff.Org.Enabled == EnabledStatusEnum.Disabled)
            throw ResultOutput.Exception("机构已禁用");

        if(userStaff.Org.Tenant.Enabled == EnabledStatusEnum.Disabled)
            throw ResultOutput.Exception("租户已禁用");

        if (userStaff.Org.Tenant.ExpiryDate < DateTime.Now.Date)
            throw ResultOutput.Exception("租户已过使用期限，请联系管理员续期");

        var userClaimInfo = new UserClaimInfo
        {
            NickName = userStaff.User.NickName ,
            UserAccount = userStaff.User.Account,
            UserId = userStaff.UserId,
            OrgId = userStaff.OrgId,
            PostId = userStaff.PostId,
            RealName = userStaff.RealName,
            TenantId = userStaff.TenantId,
            TenantType = userStaff.Org.Tenant.TenantType,
            UserStaffId = request.UserStaffId,
            UserStaffType = userStaff.UserStaffType
        };

        return _tokenBuilder.BuildToken(userClaimInfo);
    }
}

internal class RefreshTokenHandler( ITokenBuilder _tokenBuilder,ISender _sender) : IRequestHandler<RefreshTokenRequest, Token>
{
    public Task<Token> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var tokenDecodeInfo = _tokenBuilder.DecodeToken(request.Token);
        var tokenValid = _tokenBuilder.Validate(tokenDecodeInfo, out string errMsg);
        if (!tokenValid)
        {
            //校验失败
            throw ResultOutput.Exception(errMsg);
        }

        var userStaffId = tokenDecodeInfo.UserClaimInfo.UserStaffId;

        if (!userStaffId.HasValue) throw ResultOutput.Exception("员工身份信息有误");

        return _sender.Send(new RefreshTokenWithUserStaffRequest(userStaffId.Value), cancellationToken);
    }
}


internal class GetCurrentUserStaffInfoHandler(ISender _sender, ICurrentUser _currentUser,IReadRepository<UserStaffInfo> _staffRepo) 
    : IRequestHandler<GetCurrentUserStaffInfoRequest, CurrentUserStaffOutput>
{
    public async Task<CurrentUserStaffOutput> Handle(GetCurrentUserStaffInfoRequest request, CancellationToken cancellationToken)
    {
        if(!_currentUser.UserStaffId.HasValue)
            throw ResultOutput.Exception("员工身份信息有误");

        var apiPermissionScopes = await _sender.Send(new CurrentUserApiPermissionDataScopeRequest(), cancellationToken);

        var apiPermissionCodes = apiPermissionScopes?.Select(x => x.Code).Distinct().ToList() ?? new List<string>();

        var staff = await _staffRepo.SetGlobalFilterStatus(ignoreDataPermissionFilter: true)
            .SimpleFirstAsync<UserStaffPageOutput>(x => x.Id == _currentUser.UserStaffId.Value, cancellationToken);

        return new CurrentUserStaffOutput
        {
            AvatarUrl = staff?.User.AvatarUrl,
            UserName = (staff?.RealName ?? staff?.User.NickName) ?? string.Empty,
            ApiPermissionScope = apiPermissionCodes,
            TenantId = _currentUser.TenantId ?? Guid.Empty,
            TenantType = _currentUser.TenantType ?? TenantTypeEnum.NormalTenant,
            UserId = _currentUser.UserId,
            UserStaffId = _currentUser.UserStaffId ?? Guid.Empty,
            UserStaffType = _currentUser.UserStaffType ?? UserStaffTypeEnum.Normal
        };
    }
}

internal class CurrentUserStaffMenuRoutesHandler(ICurrentUser _currentUser,IReadRepository<UserStaffRole> _staffRoleRepo)
    : IRequestHandler<CurrentUserStaffMenuRoutesRequest, List<MenuRouteOutput>>
{
    public async Task<List<MenuRouteOutput>> Handle(CurrentUserStaffMenuRoutesRequest request, CancellationToken cancellationToken)
    {
        if (!_currentUser.UserStaffId.HasValue)
            throw ResultOutput.Exception("员工身份信息有误");

        var result = await _staffRoleRepo.NoDataPermissionQueryable().AsNoTracking()
            .Where(x => x.UserStaffId == _currentUser.UserStaffId && x.Role.Enabled == EnabledStatusEnum.Enabled)
            .Include(x => x.Role.RoleMenuRoutes)
            .ThenInclude(x => x.MenuRoute.View)
            .SelectMany(x => x.Role.RoleMenuRoutes.Select(rm => rm.MenuRoute))
            .ToListAsync();

        var menus = result.Select(m =>
         {
             var result = m.Adapt<MenuRouteOutput>();
             result.View = m.View?.Adapt<ViewInfoOutput>();
             return result;
         }).ToList();

        return menus;
    }
}
