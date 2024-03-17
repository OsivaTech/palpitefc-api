using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IGamesService
{
    Task<Result<GameResponse>> CreateOrUpdateAsync(GameRequest request, CancellationToken cancellationToken);
    Task<Result<GameResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<GameResponse>>> GetAsync(CancellationToken cancellationToken);
}