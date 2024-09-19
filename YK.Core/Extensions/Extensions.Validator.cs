namespace YK.Core.Extensions;

public static partial class Extensions
{
    /// <summary>
    /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="parameterName">参数名</param>
    public static void CheckNull(this object? obj, string? parameterName = null)
    {
        parameterName = parameterName ?? obj?.GetType().Name;
        ArgumentNullException.ThrowIfNull(obj, parameterName);
    }

    /// <summary>
    /// 检测对象是否为null
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    public static bool IsNull(this object? obj) => obj == null;

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsNullOrEmpty(this string? value)
        => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsNullOrEmpty(this Guid value)
        => value == Guid.Empty;

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsNullOrEmpty(this Guid? value) 
        => value == Guid.Empty || !value.HasValue;

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? value)
         => value == null || !value.Any();
}
