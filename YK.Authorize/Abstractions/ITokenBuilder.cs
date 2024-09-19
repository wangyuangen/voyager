using YK.Authorize.Models;

namespace YK.Authorize.Abstractions;

/// <summary>
/// TokenBuilder interface
/// </summary>
public interface ITokenBuilder
{
    /// <summary>
    /// 构建token
    /// </summary>
    /// <param name="claimInfo"></param>
    /// <returns></returns>
    Token BuildToken(UserClaimInfo claimInfo);
    
    /// <summary>
    /// 解析token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    TokenDecodeInfo DecodeToken(string token);

    /// <summary>
    /// token校验
    /// </summary>
    /// <param name="decodeInfo"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    bool Validate(TokenDecodeInfo decodeInfo,out string errMsg);
}
