using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IGamesService
{
    Task<Result<GameResponse>> CreateOrUpdateAsync(GameRequest request, CancellationToken cancellationToken);
    Task<Result<GameResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<GameResponse>>> GetAsync(CancellationToken cancellationToken);
}