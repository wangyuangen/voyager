using System.Reflection;

namespace YK.Infrastructure.Behaviours;

public class MediatRBehaviorConfig
{
    /// <summary>
    /// 要注入内容所在程序据
    /// </summary>
    public Assembly[] Assemblies { get; set; }

    /// <summary>
    /// 注入行为类型
    /// </summary>
    public Type BehaviorType { get; set; }
}
