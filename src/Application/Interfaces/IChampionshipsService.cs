using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IChampionshipsService
{
    Task<Result<ChampionshipResponse>> CreateOrUpdateAsync(ChampionshipRequest request, CancellationToken cancellationToken);
    Task<Result<ChampionshipResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ChampionshipResponse>>> GetAsync(CancellationToken cancellationToken);
}