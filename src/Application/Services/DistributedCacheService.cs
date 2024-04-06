using Microsoft.Extensions.Caching.Distributed;
using PalpiteFC.Api.Application.Interfaces;
using System.Text.Json;

namespace PalpiteFC.Api.Application.Services;

internal class DistributedCacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public DistributedCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetOrCreateAsync<T>(string cacheKey,
                                             Func<Task<T>> retrieveDataFunc,
                                             DateTimeOffset? absoluteExpiration = null,
                                             TimeSpan? slidingExpiration = null)
    {
        var cachedDataString = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedDataString))
        {
            return JsonSerializer.Deserialize<T>(cachedDataString)!;
        }

        var cachedData = await retrieveDataFunc();

        var serializedData = JsonSerializer.Serialize(cachedData);

        var cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = absoluteExpiration,
            SlidingExpiration = slidingExpiration
        };

        await _cache.SetStringAsync(cacheKey, serializedData, cacheEntryOptions);

        return cachedData;
    }
}
