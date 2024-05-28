using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface ILeagueService
{
    Task<Result<IEnumerable<LeagueResponse>>> GetEnabledAsync(CancellationToken cancellationToken);
    Task<Result<LeagueResponse>> UpdateAsync(int id, LeagueRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateDatabaseAsync(CancellationToken cancellationToken);
    Task<Result<LeagueResponse>> ToggleStatusAsync(int id, bool enabled, CancellationToken cancellationToken);
}