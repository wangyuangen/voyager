namespace YK.Console.Business.DataDicts;

internal class CreateDataDictValidator : AbstractValidator<CreateDataDictRequest>
{
    public CreateDataDictValidator(IReadRepository<DataDictInfo> _repo)
    {
        RuleFor(x => x.Code)
            .MustAsync(async (code, ct) => !await _repo.SimpleAnyAsync(x => x.Code == code, ct))
            .WithMessage((_,code) => $"字典编码 {code} 重复");

        RuleFor(x => x.Name)
            .MustAsync(async (inst, name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name && x.ParentCode == inst.ParentCode, ct))
            .WithMessage((_, name) => $"字典 {name} 重复");
    }
}


internal class UpdateDataDictValidator : AbstractValidator<UpdateDataDictRequest>
{
    public UpdateDataDictValidator(IReadRepository<DataDictInfo> _repo)
    {
        RuleFor(x => x.Code)
            .MustAsync(async (inst,code, ct) => !await _repo.SimpleAnyAsync(x => x.Code == code  && x.Id != inst.Id, ct))
            .WithMessage((_,code)=> $"字典编码 {code} 重复");

        RuleFor(x=>x.Name)
            .MustAsync(async(inst,name,ct)=>!await _repo.SimpleAnyAsync(x=>x.Name == name && x.ParentCode == inst.ParentCode && x.Id!=inst.Id,ct))
            .WithMessage((_, name) => $"字典 {name} 重复");
    }
}