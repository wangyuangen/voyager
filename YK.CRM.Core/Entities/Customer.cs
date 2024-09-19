namespace YK.CRM.Core.Entities;

/// <summary>
/// 客户信息
/// </summary>
public class Customer:AuditableTenantSoftDeleteEntity
{
    /// <summary>
    /// 客户姓名
    /// </summary>
    public string Name { get; set; }
}
