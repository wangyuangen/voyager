using YK.Console.Business.FileStorageInfos;
using YK.Console.Business.UserStaffInfos;

namespace YK.Console.Business.UserInfos;

internal class UpdatePersonalUserInfoHandler(ISender _sender, ICurrentUser _currentUser) : IRequestHandler<UpdatePersonalUserInfoRequest, Guid>
{
    public Task<Guid> Handle(UpdatePersonalUserInfoRequest request, CancellationToken cancellationToken)
    {
        //编辑员工
        var updateStaffRequest = request.Adapt<UpdatePersonalUserStaffReuqest>();
        _sender.Send(updateStaffRequest, cancellationToken);

        //编辑用户
        var updateUserRequest = request.Adapt<UpdateUserInfoRequest>();
        updateUserRequest.Id = _currentUser.UserId;
        return _sender.Send(updateUserRequest, cancellationToken);
    }
}

internal class UploadPersonalAvatarHandler(ISender _sender, ICurrentUser _currentUser) : IRequestHandler<UploadPersonalAvatarRequest, string>
{
    public Task<string> Handle(UploadPersonalAvatarRequest request, CancellationToken cancellationToken)
    {
        return _sender.Send(new UploadAvatarRequest
        {
            AvatarFile = request.AvatarFile,
            UserId = _currentUser.UserId,
        }, cancellationToken);
    }
}

internal class ChangePersonalPasswordHandler(ISender _sender, ICurrentUser _currentUSer) : IRequestHandler<ChangePersonalPasswordRequest, Guid>
{
    public Task<Guid> Handle(ChangePersonalPasswordRequest request, CancellationToken cancellationToken)
    {
        return _sender.Send(new ChangePasswordRequest
        {
            ConfirmPassword = request.ConfirmPassword,
            Password = request.Password,
            OldPassword = request.OldPassword,
            UserId = _currentUSer.UserId
        }, cancellationToken);
    }
}

internal class UserInfoPageHandler(IReadRepository<UserInfo> _repo) : IRequestHandler<UserInfoPageRequest, PaginationResponse<UserInfoOutput>>
{
    public Task<PaginationResponse<UserInfoOutput>> Handle(UserInfoPageRequest request, CancellationToken cancellationToken)
        => _repo.SimplePageAsync<UserInfoOutput>(request, cancellationToken: cancellationToken);
}

internal class ChangePasswordHandler(IRepository<UserInfo> repo) : IRequestHandler<ChangePasswordRequest, Guid>
{
    private readonly IRepository<UserInfo> _repo = repo;

    public async Task<Guid> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var userInfo = await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true)
            .GetByIdAsync(request.UserId, cancellationToken) 
            ?? throw ResultOutput.Exception("用户不存在");

        if (!userInfo.VerifyPassword(request.OldPassword)) throw ResultOutput.Exception("输入的旧密码不正确");
        
        userInfo.EncryptPassword(request.Password);
        await _repo.UpdateAsync(userInfo);
        return userInfo.Id;
    }
}

internal class UploadAvatarHandler(IRepository<UserInfo> _repo,ISender _sender) : IRequestHandler<UploadAvatarRequest, string>
{
    public async Task<string> Handle(UploadAvatarRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true)
                .GetByIdAsync(request.UserId, cancellationToken)
              ?? throw ResultOutput.Exception("用户不存在");

        var oldAvatarFileId = entity.AvatarFileId;

        //上传新头像
        var uploadResult = await _sender.Send(new UploadFileStorageInfoRequest
        {
            BizId = request.UserId,
            BizName = typeof(UserInfo).FullName,
            File = request.AvatarFile,
            ReName = true
        });

        if (uploadResult != null)
        {
            entity.AvatarFileId = uploadResult.Id;
            entity.AvatarUrl = uploadResult.LinkUrl;
            //更新user
            await _repo.UpdateAsync(entity, cancellationToken);

            if (oldAvatarFileId.HasValue)
            {
                //删除旧头像
                await _sender.Send(new DeleteFileStorageInfoRequest(oldAvatarFileId.Value), cancellationToken);
            }
        }

        return entity.AvatarUrl ?? string.Empty;
    }
}

internal class UpdateUserInfoHandler(IRepository<UserInfo> repo) : IRequestHandler<UpdateUserInfoRequest, Guid>
{
    private readonly IRepository<UserInfo> _repo = repo;

    public async Task<Guid> Handle(UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var userInfo = await _repo.SetGlobalFilterStatus(ignoreDataPermissionFilter:true)
            .GetByIdAsync(request.Id, cancellationToken) ?? throw ResultOutput.Exception("用户不存在");
        userInfo.Update(account: request.Account,  nickName: request.NickName,
              mobile: request.Mobile, email: request.Email,gender:request.Gender);
        await _repo.UpdateAsync(userInfo, cancellationToken);
        return userInfo.Id;
    }
}

/// <summary>
/// 创建普通用户
/// </summary>
/// <param name="repo"></param>
internal class CreateUserInfoHandler(IRepository<UserInfo> repo) : IRequestHandler<CreateUserInfoRequest, Guid>
{
    private readonly IRepository<UserInfo> _repo = repo;

    public async Task<Guid> Handle(CreateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var userInfo = request.Adapt<UserInfo>();
        var appOptions = AppCore.GetConfig<AppOptions>();
        userInfo.PasswordEncryptType = appOptions.PasswordEncryptType;
        var pwd = request.Password.IsNullOrEmpty()
            ? appOptions.DefaultPassword
            : request?.Password ?? string.Empty;
        userInfo.EncryptPassword(pwd);
        await _repo.AddAsync(userInfo, cancellationToken);
        return userInfo.Id;
    }
}
