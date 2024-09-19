using MediatR;
using YK.Console.Core.DbContext;
using YK.Infrastructure.Behaviours;

namespace YK.Console.Core.Interceptors;

public class ConsoleUnitOfWorkBehavior<TRequest, TResponse>(IConsoleUnitOfWork unitOfWork)
    : UnitOfWorkBehavior<TRequest, TResponse>(unitOfWork)
    where TRequest : IRequest<TResponse>
{
}
