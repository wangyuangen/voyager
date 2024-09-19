using YK.CRM.Core.Entities;

namespace YK.CRM.Business.Customers;

/// <summary>
/// 创建客户
/// </summary>
internal class CreateCustomerHandlers(IRepository<Customer> repo) : IRequestHandler<CreateCustomerRequest, Guid>
{
    private readonly IRepository<Customer> _repo = repo;

    public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Customer>();
        await _repo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
