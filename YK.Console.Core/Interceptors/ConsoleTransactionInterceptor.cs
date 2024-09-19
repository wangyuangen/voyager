using YK.Console.Core.DbContext;

namespace YK.Console.Core.Interceptors;

public class ConsoleTransactionAsyncInterceptor(IConsoleUnitOfWork _unitOfWork) : UowAsyncInterceptor(_unitOfWork) { }

public class ConsoleTransactionInterceptor(ConsoleTransactionAsyncInterceptor _interceptor) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        _interceptor.ToInterceptor().Intercept(invocation);
    }
}
