using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface INewsService
{
    Task<Result<NewsResponse>> CreateOrUpdateAsync(NewsRequest request);
    Task<Result<NewsResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<NewsResponse>>> GetAsync(CancellationToken cancellationToken);
}
