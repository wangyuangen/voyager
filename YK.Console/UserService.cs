using Microsoft.AspNetCore.Mvc;
using YK.Console.Business.UserInfos;
using YK.Console.Core.Consts;
using YK.Core.Attributes;

namespace YK.Console;

/// <summary>
/// 用户服务
/// </summary>
[DynamicApi(Area = ConsoleAppConsts.ApiGroup), Order(10)]
public class UserService(ISender _sender) : IDynamicApi
{
    /// <summary>
    /// 用户分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<PaginationResponse<UserInfoOutput>> PageAsync(UserInfoPageRequest request, CancellationToken cancellationToken)
        => _sender.Send(request, cancellationToken);

    /// <summary>
    /// 编辑个人信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<Guid> UpdateAsync(UpdatePersonalUserInfoRequest request, CancellationToken cancellation)
        => _sender.Send(request,cancellation);

    /// <summary>
    /// 上传个人头像
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<string> UploadAvatarAsync([FromForm]UploadPersonalAvatarRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);

    /// <summary>
    /// 修改个人密码
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowEveryUser]
    public Task<Guid> ChangePwdAsync(ChangePersonalPasswordRequest request, CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);
}
