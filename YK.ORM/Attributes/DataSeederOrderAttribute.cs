namespace YK.ORM.Attributes;

/// <summary>
/// 种子数据初始化顺序
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DataSeederOrderAttribute : Attribute 
{
    public int Order { get; private set; }

    public DataSeederOrderAttribute(int order)
    {
        this.Order = order;
    }
}
