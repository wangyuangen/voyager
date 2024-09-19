namespace YK.Console.Business.PostInfos;

internal class CreatePostInfoValidator : AbstractValidator<CreatePostInfoRequest>
{
    public CreatePostInfoValidator(IReadRepository<PostInfo> _repo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name, ct))
            .WithMessage((_, name) => $"岗位 {name} 已存在");

    }
}

internal class UpdatePostInfoValidator : AbstractValidator<UpdatePostInfoRequest>
{
    public UpdatePostInfoValidator(IReadRepository<PostInfo> _repo)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (inst, name, ct) => !await _repo.SimpleAnyAsync(x => x.Name == name && x.Id != inst.Id, ct))
            .WithMessage((_, name) => $"岗位 {name} 已存在");
    }
}

internal class DeletePostInfoValidator : AbstractValidator<DeletePostInfoRequest>
{
    public DeletePostInfoValidator(IReadRepository<UserStaffInfo> _repo)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (postId, ct) => !await _repo.SimpleAnyAsync(x => x.PostId == postId, ct))
            .WithMessage(_ => "无法删除仍存在员工的岗位");
    }
}
