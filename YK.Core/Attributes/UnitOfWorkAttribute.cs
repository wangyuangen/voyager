namespace YK.Core.Attributes;

/// <summary>
/// 工作单元-事务拦截
/// 为已经注入的接口及接口中的方法,或者Request请求类型标记特性
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class , Inherited = true)]
public class UnitOfWorkAttribute : Attribute { }