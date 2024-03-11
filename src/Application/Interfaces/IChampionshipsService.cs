using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services;

namespace PalpiteApi.Application.Interfaces;

public interface IChampionshipsService
{
    Task<ChampionshipResponse> CreateOrUpdateAsync(ChampionshipRequest request, CancellationToken cancellationToken);
    Task<ChampionshipResponse> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ChampionshipResponse>> GetAsync(CancellationToken cancellationToken);
}