using YK.Core.Contract;

namespace YK.Module.Core.Contracts;

/// <summary>
/// 业务实体信息输出
/// </summary>
public class BizInfoOutput
{
    /// <summary>
    /// 业务实体完整名称
    /// </summary>
    public string BizFullName { get; set; }

    /// <summary>
    /// 业务实体主键
    /// </summary>
    public Guid BizId { get; set; }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static BizInfoOutput Result<T>(T obj)
        where T : BaseEntity
    {
        return new BizInfoOutput
        {
            BizFullName = obj.GetType().FullName ?? obj.GetType().Name,
            BizId = obj.Id
        };
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public static BizInfoOutput Result<T>(Guid id)
    {
        return new BizInfoOutput
        {
            BizId = id,
            BizFullName = typeof(T).FullName ?? typeof(T).Name
        };
    }
}
