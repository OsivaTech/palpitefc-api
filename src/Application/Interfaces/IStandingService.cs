using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IStandingService
{
    Task<Result<ChampoionshipTeamPointsResponse>> CreateOrUpdateAsync(StandingRequest request, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ChampoionshipTeamPointsResponse>>> GetAsync(CancellationToken cancellationToken);
}
