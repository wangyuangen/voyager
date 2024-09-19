using System.Text.RegularExpressions;

namespace YK.Console.Business.UserInfos;

internal class CreateUserInfoValidator : AbstractValidator<CreateUserInfoRequest>
{
    public CreateUserInfoValidator(IReadRepository<UserInfo> repo)
    {
        RuleFor(x => x.Mobile)
            .MustAsync(async (_, mobile, ct) => !await repo.SimpleAnyAsync(x => x.Mobile == mobile, ct))
            .WithMessage((_, mobile) => $"手机号{mobile}重复");

        RuleFor(x => x.Account)
            .MustAsync(async (_, account, ct) => !await repo.SimpleAnyAsync(x => x.Account == account, ct))
            .WithMessage((_, account) => $"账户{account}重复");

        RuleFor(x => x.Password)
            .Must((_, pwd) => pwd.IsNullOrEmpty() || Regex.IsMatch(pwd??string.Empty, @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d!@#$%^&.*]{6,16}$"))
            .WithMessage((_, pwd) => "密码为字母+数字+可选特殊字符，长度在6-16之间");
    }
}

internal class UpdateUserInfoValidator : AbstractValidator<UpdateUserInfoRequest>
{
    public UpdateUserInfoValidator(IReadRepository<UserInfo> repo)
    {
        RuleFor(x => x.Mobile)
            .MustAsync(async (inst, mobile, ct) => !await repo.SimpleAnyAsync(x => x.Mobile == mobile && x.Id != inst.Id, ct))
            .WithMessage((_, mobile) => $"手机号{mobile}重复");

        RuleFor(x => x.Account)
            .MustAsync(async (inst, account, ct) => !await repo.SimpleAnyAsync(x => x.Account == account && x.Id != inst.Id, ct))
            .WithMessage((_, account) => $"账户{account}重复");
    }
}

internal class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.Password)
            .Must((_, pwd) => Regex.IsMatch(pwd, @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d!@#$%^&.*]{6,16}$"))
            .WithMessage((_, pwd) => "密码为字母+数字+可选特殊字符，长度在6-16之间")
            .Must((inst, pwd) => inst.ConfirmPassword == pwd)
            .WithMessage("两次输入的密码不一致");
    }
}