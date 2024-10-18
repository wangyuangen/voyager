using YK.Core.Commons.Tools;

namespace YK.Core.Extensions;

public static partial class Extensions
{
    /// <summary>
    /// 安全转换为字符串，去除两端空格，当值为null时返回""
    /// </summary>
    /// <param name="input">输入值</param>
    public static string SafeString(this object? input)
    {
        return input?.ToString()?.Trim() ?? string.Empty;
    }

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static byte[] ToBytes(this Stream stream)
    {
        return Converts.ToBytes(stream);
    }

    /// <summary>
    /// 转换为bool
    /// </summary>
    /// <param name="obj">数据</param>
    public static bool ToBool(this string obj)
    {
        return Converts.ToBool(obj);
    }

    /// <summary>
    /// 转换为可空bool
    /// </summary>
    /// <param name="obj">数据</param>
    public static bool? ToBoolOrNull(this string obj)
    {
        return Converts.ToBoolOrNull(obj);
    }

    /// <summary>
    /// 转换为int
    /// </summary>
    /// <param name="obj">数据</param>
    public static int ToInt(this string obj)
    {
        return Converts.ToInt(obj);
    }

    /// <summary>
    /// 转换为可空int
    /// </summary>
    /// <param name="obj">数据</param>
    public static int? ToIntOrNull(this string obj)
    {
        return Converts.ToIntOrNull(obj);
    }

    /// <summary>
    /// 转换为long
    /// </summary>
    /// <param name="obj">数据</param>
    public static long ToLong(this string obj)
    {
        return Converts.ToLong(obj);
    }

    /// <summary>
    /// 转换为可空long
    /// </summary>
    /// <param name="obj">数据</param>
    public static long? ToLongOrNull(this string obj)
    {
        return Converts.ToLongOrNull(obj);
    }

    /// <summary>
    /// 转换为double
    /// </summary>
    /// <param name="obj">数据</param>
    public static double ToDouble(this string obj)
    {
        return Converts.ToDouble(obj);
    }

    /// <summary>
    /// 转换为可空double
    /// </summary>
    /// <param name="obj">数据</param>
    public static double? ToDoubleOrNull(this string obj)
    {
        return Converts.ToDoubleOrNull(obj);
    }

    /// <summary>
    /// 转换为decimal
    /// </summary>
    /// <param name="obj">数据</param>
    public static decimal ToDecimal(this string obj)
    {
        return Converts.ToDecimal(obj);
    }

    /// <summary>
    /// 转换为可空decimal
    /// </summary>
    /// <param name="obj">数据</param>
    public static decimal? ToDecimalOrNull(this string obj)
    {
        return Converts.ToDecimalOrNull(obj);
    }

    /// <summary>
    /// 转换为日期
    /// </summary>
    /// <param name="obj">数据</param>
    public static DateTime ToDate(this string obj)
    {
        return Converts.ToDate(obj);
    }

    /// <summary>
    /// 转换为可空日期
    /// </summary>
    /// <param name="obj">数据</param>
    public static DateTime? ToDateOrNull(this string obj)
    {
        return Converts.ToDateOrNull(obj);
    }

    /// <summary>
    /// 转换为Guid
    /// </summary>
    /// <param name="obj">数据</param>
    public static Guid ToGuid(this string obj)
    {
        return Converts.ToGuid(obj);
    }

    /// <summary>
    /// 转换为可空Guid
    /// </summary>
    /// <param name="obj">数据</param>
    public static Guid? ToGuidOrNull(this string obj)
    {
        return Converts.ToGuidOrNull(obj);
    }

    /// <summary>
    /// 转换为Guid集合
    /// </summary>
    /// <param name="obj">字符串集合</param>
    public static List<Guid> ToGuidList(this IList<string> obj)
    {
        if (obj == null)
            return new List<Guid>();
        return obj.Select(t => t.ToGuid()).ToList();
    }

    /// <summary>
    /// 类型转换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T? To<T>(this object value)
    {
        return Converts.To<T>(value);
    }

    /// <summary>
    /// 转换为文件路径格式
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToPath(this string s)
    {
        if (s.IsNull())
            return string.Empty;

        return s.Replace(@"\", "/");
    }

    /// <summary>
    /// guide格式化
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static string ToUpperString(this Guid guid)
        => guid.ToString("N").ToUpper();
}
