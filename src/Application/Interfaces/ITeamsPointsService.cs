using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface ITeamsPointsService
{
    Task<Result<ChampoionshipTeamPointsResponse>> CreateOrUpdateAsync(ChampionshipTeamsPointsRequest request, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ChampoionshipTeamPointsResponse>>> GetAsync(CancellationToken cancellationToken);
}
