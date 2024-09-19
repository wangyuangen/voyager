namespace YK.Console.Business.PermissionGroupApis;

/// <summary>
/// 保存权限组接口
/// </summary>
public class SavePermissionGroupApiRequest:IRequest<bool>
{
    /// <summary>
    /// 权限组Id
    /// </summary>
    [Required]
    public Guid PermissionGroupId { get; set; }

    /// <summary>
    /// 接口Id集合
    /// </summary>
    [Required]
    public List<Guid> ApiIds { get; set; }
}

/// <summary>
/// 获取员工当前接口的数权范围
/// </summary>
public class CurrentUserApiPermissionDataScopeRequest : IRequest<List<ApiPermissionDataScope>?> { }