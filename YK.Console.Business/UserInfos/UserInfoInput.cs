namespace YK.Console.Business.UserInfos;

/// <summary>
/// 用户
/// </summary>
public class UserInfoInput
{
    /// <summary>
    /// 账号(唯一)
    /// </summary>
    [Required, MaxLength(16)]
    public string Account { get; set; } = default!;

    /// <summary>
    /// 昵称
    /// </summary>
    [Required, MaxLength(16)]
    public string NickName { get; set; } = default!;

    /// <summary>
    /// 手机号(唯一)
    /// </summary>
    [Required, MaxLength(16)]
    public string Mobile { get; set; } = default!;

    /// <summary>
    /// 邮箱
    /// </summary>
    [MaxLength(64)]
    public string? Email { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [MaxLength(16)]
    public string? Password { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [Required]
    public GenderEnum Gender { get; set; } = GenderEnum.Unknown;
}
