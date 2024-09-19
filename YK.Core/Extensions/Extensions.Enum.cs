using System.ComponentModel;
using System.Reflection;
using YK.Core.Attributes;
using YK.Core.Commons.Tools;

namespace YK.Core.Extensions;

public static partial class Extensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string ToNameWithDescription(this Enum item)
    {
        string name = item.ToString();
        var desc = item.GetType().GetField(name)?.GetCustomAttribute<DescriptionAttribute>(false);
        return $"{name}{(desc == null || desc.Description.IsNull() ? "" : $"({desc?.Description})")}";
    }

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static int Value(this Enum instance)
    {
        return EnumOperation.GetValue(instance.GetType(), instance);
    }

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="instance">枚举实例</param>
    public static TResult? Value<TResult>(this Enum instance)
    {
        return Converts.To<TResult>(Value(instance));
    }

    /// <summary>
    /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static string Description(this Enum instance)
    {
        return EnumOperation.GetDescription(instance.GetType(), instance);
    }

    /// <summary>
    /// 获取枚举的Theme
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetTheme(this Enum value)
    {
        return value.GetType().GetMember(value.ToString() ?? string.Empty).FirstOrDefault()
            ?.GetCustomAttribute<EnumThemeAttribute>()?.Theme ?? string.Empty;
    }
}
