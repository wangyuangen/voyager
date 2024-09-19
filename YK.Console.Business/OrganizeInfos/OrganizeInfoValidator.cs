namespace YK.Console.Business.OrganizeInfos;

internal class CreateOrganizeInfoValidator : AbstractValidator<CreateOrganizeInfoRequest>
{
    public CreateOrganizeInfoValidator(IReadRepository<OrganizeInfo> _repo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name, ct))
            .WithMessage(name => $"机构 {name} 不存在");
    }
}

internal class UpdateOrganizeInfoValidator : AbstractValidator<UpdateOrganizeInfoRequest>
{
    public UpdateOrganizeInfoValidator(IReadRepository<OrganizeInfo> _repo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (inst,name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name && x.Id!=inst.Id, ct))
            .WithMessage(name => $"机构 {name} 已存在");

    }
}

internal class DeleteOrganizeInfoValidator : AbstractValidator<DeleteOrganizeInfoRequest>
{
    public DeleteOrganizeInfoValidator(IReadRepository<UserStaffInfo> _usrStaffRepo)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (orgId, ct) => !await _usrStaffRepo.SimpleAnyAsync(x => x.OrgId == orgId, ct))
            .WithMessage(_ => "无法删除存在员工的机构");
    }
}
