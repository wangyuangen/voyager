namespace YK.Core.Contract;

public interface ITenant
{
    /// <summary>
    /// 所属租户Id
    /// </summary>
    Guid TenantId { get; set; }
}
