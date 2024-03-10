using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Services.Interfaces;

public interface IChampionshipsService
{
    Task<ChampionshipResponse> CreateOrUpdateAsync(ChampionshipRequest request, CancellationToken cancellationToken);
    Task<ChampionshipResponse> DeleteAsync(int id);
    Task<IEnumerable<ChampionshipResponse>> GetAsync(CancellationToken cancellationToken);
}