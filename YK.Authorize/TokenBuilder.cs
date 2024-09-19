using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using YK.Authorize.Abstractions;
using YK.Authorize.Models;
using YK.Authorize.Options;
using YK.Core.Consts;
using YK.Core.Enums;

namespace YK.Authorize;

public class TokenBuilder : ITokenBuilder
{
    public Token BuildToken(UserClaimInfo claimInfo)
    {
        var options = AppCore.GetConfig<AuthorizeOptions>();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecurityKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var timestamp = DateTime.Now.AddMinutes(options.Expires+ options.RefreshExpires).ToTimestamp().ToString();

        var claims = new List<Claim>()
        {
            new Claim(ClaimAttributes.UserId,claimInfo.UserId.SafeString()),
            new Claim(ClaimAttributes.TenantId,claimInfo.TenantId.SafeString()),
            new Claim(ClaimAttributes.UserStaffId,claimInfo.UserStaffId.SafeString()),
            new Claim(ClaimAttributes.PostId,claimInfo.PostId.SafeString()),
            new Claim(ClaimAttributes.UserAccount,claimInfo.UserAccount),
            new Claim(ClaimAttributes.RealName,claimInfo?.RealName??string.Empty),
            new Claim(ClaimAttributes.NickName,claimInfo?.NickName??string.Empty),
            new Claim(ClaimAttributes.RefreshExpires,timestamp),
            new Claim(ClaimAttributes.TenantType,claimInfo?.TenantType.SafeString() ?? string.Empty),
            new Claim(ClaimAttributes.UserStaffType,claimInfo?.UserStaffType.SafeString()?? string.Empty),
            new Claim(ClaimAttributes.OrgId,claimInfo?.OrgId.SafeString()??string.Empty)
        };

        var expires = DateTime.Now.AddMinutes(options.Expires);
        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: signingCredentials
        );

        var encodeJwt = new JwtSecurityTokenHandler().WriteToken(token);

        return new Token
        {
            TokenType = "Bearer",
            Expires = expires,
            TokenValue = encodeJwt
        };
    }

    public TokenDecodeInfo DecodeToken(string token)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(token);
        var claims = jwtSecurityToken.Claims;

        var userIdStr = claims.FindFirstValue(ClaimAttributes.UserId);
        var tenantIdStr = claims.FindFirstValue(ClaimAttributes.TenantId);
        var userStaffIdStr = claims.FindFirstValue(ClaimAttributes.UserStaffId);
        var postIdStr = claims.FindFirstValue(ClaimAttributes.PostId);
        var userAccount = claims.FindFirstValue(ClaimAttributes.UserAccount);
        var realName = claims.FindFirstValue(ClaimAttributes.RealName);
        var nickName = claims.FindFirstValue(ClaimAttributes.NickName);
        var refreshExpiresStr = claims.FindFirstValue(ClaimAttributes.RefreshExpires);
        var userStaffTypeStr = claims.FindFirstValue(ClaimAttributes.UserStaffType);
        var tenantTypeStr = claims.FindFirstValue(ClaimAttributes.TenantType);
        var orgIdStr = claims.FindFirstValue(ClaimAttributes.OrgId);

        var decodeInfo = new TokenDecodeInfo
        {
            Claims = claims,
            RawHeader = jwtSecurityToken.RawHeader,
            RawPayload = jwtSecurityToken.RawPayload,
            RawSignature = jwtSecurityToken.RawSignature,
            RefreshExpires = refreshExpiresStr?.ToLongOrNull(),
            UserClaimInfo = new UserClaimInfo
            {
                RealName = realName,
                UserAccount = userAccount ?? string.Empty,
                PostId = postIdStr?.ToGuidOrNull(),
                UserStaffId = userStaffIdStr?.ToGuidOrNull(),
                TenantId = tenantIdStr?.ToGuidOrNull(),
                UserId = userIdStr?.ToGuid() ?? Guid.Empty,
                NickName = nickName ?? string.Empty,
                TenantType = userStaffTypeStr?.To<TenantTypeEnum>(),
                UserStaffType = userStaffTypeStr?.To<UserStaffTypeEnum>(),
                OrgId = orgIdStr?.ToGuidOrNull()
            }
        };

        return decodeInfo;
    }

    public bool Validate(TokenDecodeInfo decodeInfo, out string errMsg)
    {
        errMsg = string.Empty;
        bool validatePass = false;
        var nowTimestamp = DateTime.Now.ToTimestamp();

        errMsg = decodeInfo.Claims?.Any() is false ? "token 解析校验失败" : errMsg;
        errMsg = !decodeInfo.RefreshExpires.HasValue || decodeInfo.RefreshExpires <= nowTimestamp ? "登录信息已过期" : errMsg;
        errMsg = decodeInfo.UserClaimInfo.UserId == Guid.Empty  ? "用户信息获取失败" : errMsg;

        var options = AppCore.GetConfig<AuthorizeOptions>();
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.SecurityKey)), SecurityAlgorithms.HmacSha256);
        var input = $"{decodeInfo.RawHeader}.{decodeInfo.RawPayload}";
        errMsg = decodeInfo.RawSignature != JwtTokenUtilities.CreateEncodedSignature(input, signingCredentials)
                ? "token 验签失败"
                : errMsg;

        validatePass = errMsg.IsNullOrEmpty();

        return validatePass;
    }
}