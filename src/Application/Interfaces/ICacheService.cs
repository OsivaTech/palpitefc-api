namespace PalpiteFC.Api.Application.Interfaces;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string cacheKey,
                                Func<Task<T>> retrieveDataFunc,
                                TimeSpan? slidingExpiration = null,
                                DateTimeOffset? absoluteExpiration = null);
}