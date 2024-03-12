using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IChampionshipsService
{
    Task<Result<ChampionshipResponse>> CreateOrUpdateAsync(ChampionshipRequest request, CancellationToken cancellationToken);
    Task<Result<ChampionshipResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ChampionshipResponse>>> GetAsync(CancellationToken cancellationToken);
}