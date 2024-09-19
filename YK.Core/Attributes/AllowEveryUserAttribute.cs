namespace YK.Core.Attributes;

/// <summary>
/// 允许每位用户访问
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AllowEveryUserAttribute : Attribute { }
