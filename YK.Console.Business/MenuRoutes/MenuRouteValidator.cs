namespace YK.Console.Business.MenuRoutes;

internal class CreateMenuRouteValidator : AbstractValidator<CreateMenuRouteRequest>
{
    public CreateMenuRouteValidator(IReadRepository<MenuRouteInfo> _repo)
    {
        RuleFor(x => x)
            .MustAsync(async (inst, ct) => !await _repo.SimpleAnyAsync(x => x.Name == inst.Name || x.RouteUrl == inst.RouteUrl, ct))
            .WithMessage(_=> "菜单已存在");
    }
}

internal class UpdateMenuRouteValidator : AbstractValidator<UpdateMenuRouteRequest>
{
    public UpdateMenuRouteValidator(IReadRepository<MenuRouteInfo> _repo)
    {
        RuleFor(x => x)
            .MustAsync(async(inst,ct)=>!await _repo.SimpleAnyAsync(x=>x.Id != inst.Id && (x.Name == inst.Name || x.RouteUrl == inst.RouteUrl),ct))
               .WithMessage(_ => "菜单已存在");
    }
}

internal class DeleteMenuRouteValidator : AbstractValidator<DeleteMenuRouteRequest>
{
    public DeleteMenuRouteValidator(IReadRepository<RoleMenuRoute> _repo)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (menuId, ct) => !await _repo.SimpleAnyAsync(x => x.MenuRouteId == menuId, ct))
            .WithMessage(_ => "无法删除已被角色绑定的菜单");
    }
}
