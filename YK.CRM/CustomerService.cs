using YK.CRM.Business.Customers;
using YK.CRM.Core.Consts;

namespace YK.CRM;

/// <summary>
/// 客户服务
/// </summary>
[DynamicApi(Area = CrmAppConsts.ApiGroup),Order(10)]
public class CustomerService(ISender sender):IDynamicApi
{
    private readonly ISender _sender = sender;

    /// <summary>
    /// 创建客户
    /// </summary>
    /// <returns></returns>
    public Task<Guid> CreateAsync(CreateCustomerRequest request,CancellationToken cancellationToken)
        => _sender.Send(request,cancellationToken);
    
}
