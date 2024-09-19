using YK.CRM.Core.DbContext;

namespace YK.CRM.Core.Interceptors;

public class CrmTransactionAsyncInterceptor(ICrmUnitOfWork _unitOfWork) : UowAsyncInterceptor(_unitOfWork) { }

public class CrmTransactionInterceptor(CrmTransactionAsyncInterceptor _interceptor) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        _interceptor.ToInterceptor().Intercept(invocation);
    }
}