namespace YK.CRM.Business.Customers;

/// <summary>
/// 创建客户命令
/// </summary>
public class CreateCustomerRequest : CustomerInput, IRequest<Guid> { }
