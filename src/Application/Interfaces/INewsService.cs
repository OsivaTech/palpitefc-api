using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface INewsService
{
    Task<Result<NewsResponse>> CreateOrUpdateAsync(NewsRequest request);
    Task<Result<NewsResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<NewsResponse>>> GetAsync(CancellationToken cancellationToken);
}
