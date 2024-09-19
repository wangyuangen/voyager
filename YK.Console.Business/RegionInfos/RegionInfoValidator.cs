namespace YK.Console.Business.RegionInfos;

internal class CreateRegionInfoValidator : AbstractValidator<CreateRegionInfoRequest>
{
    public CreateRegionInfoValidator(IReadRepository<RegionInfo> _repo)
    {
        RuleFor(x => x.Code)
            .MustAsync(async (code, ct) => !await _repo.SimpleAnyAsync(x => x.Code == code, ct))
            .WithMessage((_, code) => $"行政区域代码 {code} 已存在");

        RuleFor(x => x.Name)
            .MustAsync(async (inst, name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name && x.ParentCode == inst.ParentCode, ct))
            .WithMessage((_, name) => $"行政区域 {name} 已存在");
    }
}

internal class UpdateRegionInfoValidator : AbstractValidator<UpdateRegionInfoRequest>
{
    public UpdateRegionInfoValidator(IReadRepository<RegionInfo> _repo)
    {
        RuleFor(x => x.Code)
            .MustAsync(async(inst,code,ct)=>!await _repo.SimpleAnyAsync(x=>x.Code == code && x.Id != inst.Id,ct))
            .WithMessage((_, code) => $"行政区域代码 {code} 已存在");

        RuleFor(x=>x.Name)
            .MustAsync(async (inst,name,ct)=>!await _repo.SimpleAnyAsync(x=>x.Name == name && x.ParentCode == inst.ParentCode && x.Id!=inst.Id,ct))
             .WithMessage((_, name) => $"行政区域 {name} 已存在");
    }
}
