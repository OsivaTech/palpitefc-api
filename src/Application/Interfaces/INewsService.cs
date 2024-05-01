using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface INewsService
{
    Task<Result<IEnumerable<NewsResponse>>> GetAsync(CancellationToken cancellationToken);
    Task<Result<NewsResponse>> CreateAsync(NewsRequest request, CancellationToken cancellationToken);
    Task<Result<NewsResponse>> UpdateAsync(int id, NewsRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
