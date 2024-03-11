using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Interfaces;

public interface IGamesService
{
    Task<IEnumerable<GameResponse>> GetAsync(CancellationToken cancellationToken);
}