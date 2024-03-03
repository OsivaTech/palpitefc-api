using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Services.Interfaces;

public interface IGamesService
{
    Task<IEnumerable<GameResponse>> GetAsync(CancellationToken cancellationToken);
}