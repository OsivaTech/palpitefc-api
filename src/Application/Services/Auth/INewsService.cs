
using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Services.Auth;

public interface INewsService
{
    Task<NewsResponse> CreateOrUpdateAsync(NewsRequest request);
    Task<IEnumerable<NewsResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<NewsResponse>> GetAsync(CancellationToken cancellationToken);
}
