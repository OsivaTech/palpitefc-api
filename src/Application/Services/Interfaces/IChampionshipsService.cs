using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Services.Interfaces;

public interface IChampionshipsService
{
    Task<IEnumerable<ChampionshipResponse>> GetAsync(CancellationToken cancellationToken);
}