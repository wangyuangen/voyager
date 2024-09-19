using YK.Core.Extensions;

namespace YK.Cache.Consts;

public class CacheConsts
{
    /// <summary>
    /// 获取缓存键
    /// </summary>
    /// <typeparam name="T">缓存对象类型</typeparam>
    /// <param name="tenantId">租户id</param>
    /// <param name="bizId">业务id</param>
    /// <returns></returns>
    public static string GetKey<T>(object? tenantId = null, object? bizId = null)
    {
        string key = typeof(T).Name;
        if (!tenantId.IsNull()) key = $"{key}:{tenantId}";
        if (!bizId.IsNull()) key = $"{key}:{bizId}";
        return key;
    }
}
