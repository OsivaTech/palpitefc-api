namespace PalpiteFC.Api.Application.Interfaces;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string cacheKey,
                                Func<Task<T>> retrieveDataFunc,
                                DateTimeOffset? absoluteExpiration = null,
                                TimeSpan? slidingExpiration = null);
}