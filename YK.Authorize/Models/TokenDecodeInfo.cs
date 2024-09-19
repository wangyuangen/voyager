namespace YK.Authorize.Models;

/// <summary>
/// token解析信息
/// </summary>
public class TokenDecodeInfo
{
    /// <summary>
    /// claims
    /// </summary>
    public IEnumerable<Claim>? Claims { get; set; }

    /// <summary>
    /// 过期时间戳
    /// </summary>
    public long? RefreshExpires { get; set; }

    /// <summary>
    /// header
    /// </summary>
    public string RawHeader { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string RawPayload { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public string RawSignature { get; set; }

    /// <summary>
    /// 用户身份信息
    /// </summary>
    public UserClaimInfo UserClaimInfo { get; set; }
}
