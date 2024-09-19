namespace YK.Console.Core.Enums;

/// <summary>
/// 地区级别
/// </summary>
[Description("地区级别")]
public enum RegionLevel
{
    /// <summary>
    /// 省份
    /// </summary>
    [Description("省份")]
    Province=1,

    /// <summary>
    /// 城市
    /// </summary>
    [Description("城市")]
    City=2,

    /// <summary>
    /// 县/区
    /// </summary>
    [Description("县/区")]
    County=3,

    /// <summary>
    /// 乡镇/街道
    /// </summary>
    [Description("乡镇/街道")]
    Town=4,

    /// <summary>
    /// 村/村委会/社区/居委会
    /// </summary>
    [Description("村/社区")]
    Vilage=5
}
