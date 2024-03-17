using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface ITeamsPointsService
{
    Task<Result<ChampoionshipTeamPointsResponse>> CreateOrUpdateAsync(ChampionshipTeamsPointsRequest request, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ChampoionshipTeamPointsResponse>>> GetAsync(CancellationToken cancellationToken);
}
