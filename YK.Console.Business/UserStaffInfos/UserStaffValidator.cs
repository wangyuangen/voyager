using System.Text.RegularExpressions;

namespace YK.Console.Business.UserStaffInfos;

internal class CreateUserStaffValidator : AbstractValidator<CreateUserStaffRequest>
{
    public CreateUserStaffValidator(IReadRepository<UserStaffInfo> _repo)
    {
        RuleFor(x => x.JobNo)
            .MustAsync(async (jobNo, ct) => !await _repo.SimpleAnyAsync(x => x.JobNo == jobNo, ct))
            .WithMessage(jobNo => $"工号 {jobNo} 重复");

        RuleFor(x => x.UserInfo.Password)
            .Must((_, pwd) => pwd.IsNullOrEmpty() || Regex.IsMatch(pwd ?? string.Empty, @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d!@#$%^&.*]{6,16}$"))
            .WithMessage((_, pwd) => "密码为字母+数字+可选特殊字符，长度在6-16之间");
    }
}


internal class UpdateUserStaffValidator : AbstractValidator<UpdateUserStaffRequest>
{
    public UpdateUserStaffValidator(IReadRepository<UserStaffInfo> _repo)
    {
        RuleFor(x => x.JobNo)
           .MustAsync(async (inst, jobNo, ct) => !await _repo.SimpleAnyAsync(x => x.JobNo == jobNo && x.Id != inst.Id, ct))
           .WithMessage((_, jobNo) => $"工号 {jobNo} 重复");
    }
}

internal class DeleteUserStaffValidator : AbstractValidator<DeleteUserStaffRequest>
{
    public DeleteUserStaffValidator(IReadRepository<UserStaffInfo> _repo)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, ct) => !await _repo.SimpleAnyAsync(x => x.Id == id && x.Enabled == EnabledStatusEnum.Enabled, ct))
            .WithMessage(_ => $"无法删除处于 {EnabledStatusEnum.Enabled.Description()} 的员工")
            .MustAsync(async (id, ct) => !await _repo.SimpleAnyAsync(x => x.Id == id && x.UserStaffType != UserStaffTypeEnum.Normal, ct))
            .WithMessage(_ => $"无法删除管理员");
    }
}
