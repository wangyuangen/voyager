namespace YK.Notice.Consts;

public class NoticeConsts
{
    /// <summary>
    /// 组名称
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static string TenantGroupName(Guid? tenantId) => $"GROUP_ONLINE_{tenantId ?? Guid.Empty}";

    /// <summary>
    /// 下线通知
    /// </summary>
    public const string ForceOfflineContent = "强制下线";
}
