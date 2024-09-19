namespace YK.Console.Business.Abstractors;

/// <summary>
/// 租户后台任务
/// </summary>
[UnitOfWork]
public interface ITenantJob : IScopedService
{
    /// <summary>
    /// 租户初始化
    /// </summary>
    /// <param name="tenantInfo"></param>
    /// <param name="enabled"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("租户初始化")]
    Task InitialAsync(TenantInfo tenantInfo, CancellationToken cancellationToken = default);
}
