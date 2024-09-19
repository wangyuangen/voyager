namespace YK.Console.Business.RegionInfos;

public class RegionInfoSimpleOuput:BaseOutput
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 热门
    /// </summary>
    public bool Hot { get; set; }
}
