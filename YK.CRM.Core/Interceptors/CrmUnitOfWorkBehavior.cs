using MediatR;
using YK.CRM.Core.DbContext;
using YK.Infrastructure.Behaviours;

namespace YK.CRM.Core.Interceptors;

public class CrmUnitOfWorkBehavior<TRequest, TResponse>(ICrmUnitOfWork unitOfWork)
    : UnitOfWorkBehavior<TRequest, TResponse>(unitOfWork)
     where TRequest : IRequest<TResponse>
{
}
