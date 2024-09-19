namespace YK.Console.Business.Roles;

internal class CreateRoleInfoValidator : AbstractValidator<CreateRoleInfoRequest>
{
    public CreateRoleInfoValidator(IReadRepository<RoleInfo> _repo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (inst, name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name, ct))
            .WithMessage((_, name) => $"角色 {name} 已存在");

        RuleFor(x => x.Enabled)
           .Must(x => x == EnabledStatusEnum.Disabled)
           .WithMessage(_ => "启用角色前请至少为其授权相应的菜单");
    }
}

internal class UpdateRoleInfoValidator : AbstractValidator<UpdateRoleInfoRequest>
{
    public UpdateRoleInfoValidator(IReadRepository<RoleInfo> _repo,IReadRepository<RoleMenuRoute> _rmRepo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (inst,name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name && x.Id!=inst.Id, ct))
            .WithMessage((_,name) => $"角色 {name} 已存在");

        RuleFor(x => x.Enabled)
             .MustAsync(async (inst, status, ct) => await _rmRepo.SimpleAnyAsync(x => x.RoleId == inst.Id, ct) || status == EnabledStatusEnum.Disabled)
             .WithMessage(_ => "启用角色前请至少为其授权相应的菜单");
    }
}

internal class DeleteRoleInfoValidator : AbstractValidator<DeleteRoleInfoRequest>
{
    public DeleteRoleInfoValidator(IReadRepository<UserStaffRole> _staffRoleRepo)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (roleId, ct) => !await _staffRoleRepo.SimpleAnyAsync(x => x.RoleId == roleId, ct))
            .WithMessage(_ => "无法删除已被员工所使用的角色");
    }
}

