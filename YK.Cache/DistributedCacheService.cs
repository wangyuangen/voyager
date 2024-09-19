using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text;
using YK.Cache.Abstractions;
using YK.Cache.Options;
using YK.Core;
using YK.Core.Commons;

namespace YK.Cache;

public class DistributedCacheService(IDistributedCache _cache,
    ILogger<DistributedCacheService> _logger,
    ISerializerService _serializer) :ICacheManager
{

    public T? Get<T>(string key) =>
        Get(key) is { } data
            ? Deserialize<T>(data)
            : default;

    private byte[]? Get(string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        try
        {
            return _cache.Get(key);
        }
        catch
        {
            return null;
        }
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken token = default) =>
        await GetAsync(key, token) is { } data
            ? Deserialize<T>(data)
            : default;

    private async Task<byte[]?> GetAsync(string key, CancellationToken token = default)
    {
        try
        {
            return await _cache.GetAsync(key, token);
        }
        catch
        {
            return null;
        }
    }

    public void Refresh(string key)
    {
        try
        {
            _cache.Refresh(key);
        }
        catch
        {
        }
    }

    public async Task RefreshAsync(string key, CancellationToken token = default)
    {
        try
        {
            await _cache.RefreshAsync(key, token);
            _logger.LogDebug(string.Format("Cache Refreshed : {0}", key));
        }
        catch
        {
        }
    }

    public void Remove(string key)
    {
        try
        {
            _cache.Remove(key);
        }
        catch
        {
        }
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        try
        {
            await _cache.RemoveAsync(key, token);
        }
        catch
        {
        }
    }

    public void Set<T>(string key, T value, TimeSpan? slidingExpiration = null) =>
        Set(key, Serialize(value), slidingExpiration);

    private void Set(string key, byte[] value, TimeSpan? slidingExpiration = null)
    {
        try
        {
            _cache.Set(key, value, GetOptions(slidingExpiration));
            _logger.LogDebug($"Added to Cache : {key}");
        }
        catch
        {
        }
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default) =>
        SetAsync(key, Serialize(value), slidingExpiration, cancellationToken);

    private async Task SetAsync(string key, byte[] value, TimeSpan? slidingExpiration = null, CancellationToken token = default)
    {
        try
        {
            await _cache.SetAsync(key, value, GetOptions(slidingExpiration), token);
            _logger.LogDebug($"Added to Cache : {key}");
        }
        catch
        {
        }
    }

    private byte[] Serialize<T>(T item) =>
        Encoding.Default.GetBytes(_serializer.Serialize(item));

    private T Deserialize<T>(byte[] cachedData) =>
        _serializer.Deserialize<T>(Encoding.Default.GetString(cachedData));

    private static DistributedCacheEntryOptions GetOptions(TimeSpan? slidingExpiration)
    {
        var options = new DistributedCacheEntryOptions();

        if (!slidingExpiration.HasValue)
        {
            slidingExpiration = TimeSpan.FromMinutes(60);

            var cacheOptions = AppCore.GetConfig<CacheOptions>();

            if (cacheOptions.DefaultExpirationForHour.HasValue)
                slidingExpiration = TimeSpan.FromHours(cacheOptions.DefaultExpirationForHour.Value);
        }

        options.SetSlidingExpiration(slidingExpiration.Value);

        return options;
    }
}
