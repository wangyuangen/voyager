namespace YK.Console.Business.Tenants;

internal class CreateTenantValidator : AbstractValidator<CreateTenantRequest>
{
    public CreateTenantValidator(IReadRepository<TenantInfo> readRepo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (_, name, ct) => !await readRepo.SimpleAnyAsync(x => x.Name == name, ct))
            .WithMessage((_, name) => $"租户名称{name}重复");
    }
}

internal class UpdateTenantValidator : AbstractValidator<UpdateTenantRequest>
{
    public UpdateTenantValidator(IReadRepository<TenantInfo> _repo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (inst, name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name && x.Id != inst.Id, ct))
            .WithMessage((_, name) => $"租户名称{name}重复");
    }
}