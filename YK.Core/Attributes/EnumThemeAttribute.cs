namespace YK.Core.Attributes;

/// <summary>
/// 枚举样式
/// </summary>
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumThemeAttribute:Attribute
{
    public string Theme { get; private set; }

    public EnumThemeAttribute(string theme)
    {
        this.Theme = theme;
    }
}
