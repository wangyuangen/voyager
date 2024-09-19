using Microsoft.AspNetCore.Identity;
using YK.ORM.Attributes;

namespace YK.Console.Core.Entities;

/// <summary>
/// 用户信息
/// </summary>
[DataSeederOrder(10)]
public class UserInfo:AuditableSoftDeleteEntity
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

    /// <summary>
    /// 用户员工列表
    /// </summary>
    public virtual List<UserStaffInfo> UserStaffInfos { get; set; }

    /// <summary>
    /// 编辑
    /// </summary>
    public UserInfo Update(string? account = null,string? nickName = null,string? password = null,PasswordEncryptType? passwordEncryptType=null,
        string? mobile = null,string? email = null,Guid? avatarFileId = null,string? avatarUrl = null,GenderEnum? gender = null)
    {
        if (account is not null && Account?.Equals(account) is not true)
            Account = account;

        if(nickName is not null && NickName?.Equals(nickName) is not true)
            NickName = nickName;

        if(password is not null && Password?.Equals(password) is not true)
            Password = password;

        if (passwordEncryptType.HasValue && PasswordEncryptType != passwordEncryptType)
            PasswordEncryptType = passwordEncryptType.Value;

        if(mobile is not null && Mobile?.Equals(mobile)is not true)
            Mobile = mobile;

        if(email is not null && Email?.Equals(email)is not true)
            Email = email;

        if(avatarFileId.HasValue && AvatarFileId!=avatarFileId)
            AvatarFileId = avatarFileId.Value;

        if(avatarUrl is not null && AvatarUrl?.Equals(avatarUrl) is not true)
            AvatarUrl = avatarUrl;

        if (gender.HasValue && Gender != gender)
            Gender = gender.Value;

        return this;
    }

    /// <summary>
    /// 密码加密
    /// </summary>
    /// <param name="originalPwd"></param>
    public UserInfo EncryptPassword(string originalPwd)
    {
        var _pwdHasher = AppCore.GetRequiredService<IPasswordHasher<UserInfo>>();
        Password = PasswordEncryptType switch
        {
            PasswordEncryptType.MD5Encrypt32 => MD5Encrypt.Encrypt32(originalPwd),
            PasswordEncryptType.PasswordHasher => _pwdHasher.HashPassword(this, originalPwd),
            _ => string.Empty
        } ?? string.Empty;
        return this;
    }

    /// <summary>
    /// 密码校验
    /// </summary>
    /// <param name="originalPwd"></param>
    /// <returns></returns>
    public bool VerifyPassword(string originalPwd)
    {
        var _pwdHasher = AppCore.GetRequiredService<IPasswordHasher<UserInfo>>();
        if (PasswordEncryptType == PasswordEncryptType.MD5Encrypt32)
        {
            var pwd = MD5Encrypt.Encrypt32(originalPwd);
            return pwd == Password;
        }
        var verifyResult = _pwdHasher.VerifyHashedPassword(this, Password, originalPwd);
        return verifyResult == PasswordVerificationResult.Success || verifyResult == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
