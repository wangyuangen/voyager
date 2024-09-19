using System.ComponentModel;

namespace YK.Core.Enums;

/// <summary>
/// 密码加密类型
/// </summary>
[Description("密码加密类型")]
public enum PasswordEncryptType
{
    /// <summary>
    /// 32位MD5加密
    /// </summary>
    [Description("32位MD5加密")]
    MD5Encrypt32 = 0,

    /// <summary>
    /// 标准标识密码哈希
    /// </summary>
    [Description("标准标识密码哈希")]
    PasswordHasher = 1,
}
