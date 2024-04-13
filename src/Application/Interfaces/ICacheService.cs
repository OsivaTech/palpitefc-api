
namespace PalpiteFC.Api.Application.Interfaces;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> retrieveDataFunc, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null, CancellationToken cancellationToken = default);
    Task CreateAsync<T>(string cacheKey, T data, TimeSpan? slidingExpiration = null, DateTimeOffset? absoluteExpiration = null, CancellationToken cancellationToken = default);
    Task<bool> ExistsKey(string cacheKey, CancellationToken cancellationToken = default);
}