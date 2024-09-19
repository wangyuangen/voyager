namespace YK.Console.Business.ViewInfos;

internal class CreateViewInfoValidator : AbstractValidator<CreateViewInfoRequest>
{
    public CreateViewInfoValidator(IReadRepository<ViewInfo> _repo)
    {
        RuleFor(x => x)
            .MustAsync(async (inst, ct) => !await _repo.SimpleAnyAsync(x => x.RouteName == inst.RouteName || x.Name == inst.Name || x.Path == inst.Path, ct))
            .WithMessage(_ => "视图已存在");
    }
}

internal class UpdateViewInfoValidator : AbstractValidator<UpdateViewInfoRequest>
{
    public UpdateViewInfoValidator(IReadRepository<ViewInfo> _repo)
    {
        RuleFor(x => x)
            .MustAsync(async(inst,ct)=>!await _repo.SimpleAnyAsync(x=>x.Id!=inst.Id && (x.RouteName == inst.RouteName || x.Name == inst.Name || x.Path == inst.Path),ct))
            .WithMessage(_ => "视图已存在");
    }
}

internal class DeleteViewInfoValidator : AbstractValidator<DeleteViewInfoRequest>
{
    public DeleteViewInfoValidator(IReadRepository<MenuRouteInfo> _repo)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (viewId, ct) => !await _repo.SimpleAnyAsync(x => x.ViewId == viewId, ct))
            .WithMessage(_ => "无法删除已绑定菜单的试图");
    }
}
