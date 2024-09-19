namespace YK.CRM.Business.Customers;

/// <summary>
/// 创建客户
/// </summary>
public class CustomerInput
{
    /// <summary>
    /// 姓名
    /// </summary>
    [Required, MaxLength(64)]
    public string Name { get; set; }
}
