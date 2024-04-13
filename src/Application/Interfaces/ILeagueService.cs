using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface ILeagueService
{
    Task<Result<LeagueResponse>> CreateOrUpdateAsync(LeagueRequest request, CancellationToken cancellationToken);
    Task<Result<LeagueResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<LeagueResponse>>> GetAsync(CancellationToken cancellationToken);
}