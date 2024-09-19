namespace YK.Cache.Options;

public class CacheOptions
{
    /// <summary>
    /// 使用分布式缓存
    /// </summary>
    public bool UseDistributedCache { get; set; }
    /// <summary>
    /// 启用redis背板
    /// </summary>
    public bool PreferRedis { get; set; }
    /// <summary>
    /// redis host
    /// </summary>
    public string? RedisURL { get; set; }
    /// <summary>
    /// redis授权认证密码
    /// </summary>
    public string? RedisAuth { get; set; }
    /// <summary>
    /// 默认过期时间(h)
    /// </summary>
    public int? DefaultExpirationForHour { get; set; }
}
