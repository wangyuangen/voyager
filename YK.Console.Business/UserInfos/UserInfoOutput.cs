namespace YK.Console.Business.UserInfos;

/// <summary>
/// 用户信息输出
/// </summary>
public class UserInfoOutput : AuditableOutput
{
    /// <summary>
    /// 账号(唯一)
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 加密类型
    /// </summary>
    public PasswordEncryptType PasswordEncryptType { get; set; }

    /// <summary>
    /// 手机号(唯一)
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 头像文件Id
    /// </summary>
    public Guid? AvatarFileId { get; set; }

    /// <summary>
    /// 头像外链
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public GenderEnum Gender { get; set; }
}
