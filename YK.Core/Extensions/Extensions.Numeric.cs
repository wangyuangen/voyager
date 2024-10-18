namespace YK.Core.Extensions;

public static partial class Extensions
{
    /// <summary>
    /// 除法
    /// </summary>
    /// <param name="divisor">除数</param>
    /// <param name="dividend">被除数</param>
    /// <param name="precision">精度(四舍五入)</param>
    /// <returns></returns>
    public static float Division(this float divisor, float dividend, int precision = 2)
        => dividend == 0 ? 0 : MathF.Round(divisor / dividend, precision);

    /// <summary>
    /// 乘法
    /// </summary>
    /// <param name="num1">数值1</param>
    /// <param name="num2">数值2 </param>
    /// <param name="precision">精度(四舍五入)</param>
    /// <returns></returns>
    public static float Multiplication(this float num1, float num2, int precision = 2)
        => MathF.Round(num1 * num2, precision);

    /// <summary>
    /// 精度格式化
    /// </summary>
    /// <param name="number"></param>
    /// <param name="precision">精度(四舍五入)</param>
    /// <returns></returns>
    public static float PrecisionFormat(this float number, int precision = 2)
        => MathF.Round(number, precision);
}
