using YK.Core.Attributes;
using YK.ORM.Abstractions;

namespace YK.Infrastructure.Behaviours;

public abstract class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork _unitOfWork) : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!request.GetType().HasAttribute<UnitOfWorkAttribute>()) return await next();

        TResponse response;

        try
        {
            _unitOfWork.BeginTransaction();

            response = await next();

            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            _unitOfWork.Dispose();
        }

        return response;
    }
}
