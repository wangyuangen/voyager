namespace YK.Console.Business.ApiInfos;

internal class ApiInfoEnabledStatusChangeValidator : AbstractValidator<ApiInfoEnabledStatusChangeRequest>
{
    public ApiInfoEnabledStatusChangeValidator(IReadRepository<ApiInfo> _repo)
    {
        RuleFor(x => x.Enabled)
            .MustAsync(async (inst, status, ct) => await _repo.SimpleAnyAsync(x => x.Id == inst.Id && x.Enabled != status, ct))
            .WithMessage((_, status) => $"当前状态已经是 {status.Description()},请勿重复操作");
    }
}
