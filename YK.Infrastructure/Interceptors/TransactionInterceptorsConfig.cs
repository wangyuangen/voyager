using System.Reflection;

namespace YK.Infrastructure.Interceptors;

/// <summary>
/// 事务注入配置
/// </summary>
public class TransactionInterceptorsConfig
{
    /// <summary>
    /// 要注入内容所在程序据
    /// </summary>
    public Assembly[] Assemblies { get; set; }

    /// <summary>
    /// 注入拦截器类型
    /// </summary>
    public List<Type> InterceptorTypes { get; set; } = new List<Type>();
}
