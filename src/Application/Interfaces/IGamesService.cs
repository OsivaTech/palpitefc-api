using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IGamesService
{
    Task<Result<IEnumerable<GameResponse>>> GetAsync(CancellationToken cancellationToken);
}