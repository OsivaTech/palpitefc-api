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
                                             TimeSpan? slidingExpiration = null,
                                             DateTimeOffset? absoluteExpiration = null,
                                             CancellationToken cancellationToken = default)
    {
        var cachedDataString = await _cache.GetStringAsync(cacheKey, cancellationToken);

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

        await _cache.SetStringAsync(cacheKey, serializedData, cacheEntryOptions, cancellationToken);

        return cachedData;
    }

    public async Task<bool> ExistsKey(string cacheKey, CancellationToken cancellationToken = default)
        => await _cache.GetAsync(cacheKey, cancellationToken) is not null;

    public async Task CreateAsync<T>(string cacheKey,
                                     T data,
                                     TimeSpan? slidingExpiration = null,
                                     DateTimeOffset? absoluteExpiration = null,
                                     CancellationToken cancellationToken = default)
    {
        var serializedData = JsonSerializer.Serialize(data);

        var cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = absoluteExpiration,
            SlidingExpiration = slidingExpiration
        };

        await _cache.SetStringAsync(cacheKey, serializedData, cacheEntryOptions, cancellationToken);
    }
}
