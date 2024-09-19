namespace YK.Console.Business.PermissionGroups;

internal class CreatePermissionGroupValidator : AbstractValidator<CreatePermissionGroupRequest>
{
    public CreatePermissionGroupValidator(IReadRepository<PermissionGroup> _repo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name, ct))
            .WithMessage(_ => "权限组已存在");
    }
}

internal class UpdatePermissionGroupValidator : AbstractValidator<UpdatePermissionGroupRequest>
{
    public UpdatePermissionGroupValidator(IReadRepository<PermissionGroup> _repo)
    {
        RuleFor(x=>x.Name)
             .MustAsync(async (inst, name,ct) => !await _repo.SimpleAnyAsync(x => x.Id!=inst.Id && x.Name == name ,ct))
            .WithMessage(_ => "权限组已存在");
    }
}

internal class DeletePermissionGroupValidator : AbstractValidator<DeletePermissionGroupRequest>
{
    public DeletePermissionGroupValidator(IReadRepository<RolePermissionGroup> _rpRepo)
    {
        RuleFor(x => x.Id)
           .MustAsync(async (id, ct) => !await _rpRepo.SimpleAnyAsync(x => x.PermissionGroupId == id, ct))
           .WithMessage(_ => "无法删除已被角色绑定的权限组");
    }
}
