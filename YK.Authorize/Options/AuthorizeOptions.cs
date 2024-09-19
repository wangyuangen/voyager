﻿namespace YK.Authorize.Options;

public class AuthorizeOptions 
{
    /// <summary>
    /// 发行者
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 订阅者
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// 密钥
    /// </summary>
    public string SecurityKey { get; set; }

    /// <summary>
    /// 有效期(分钟)
    /// </summary>
    public int Expires { get; set; }

    /// <summary>
    /// 刷新有效期(分钟)
    /// </summary>
    public int RefreshExpires { get; set; }
}

