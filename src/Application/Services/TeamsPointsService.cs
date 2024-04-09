using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class TeamsPointsService : ITeamsPointsService
{
    private readonly IStandingsRepository _repository;

    public TeamsPointsService(IStandingsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ChampoionshipTeamPointsResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var teamsPoints = await _repository.Select();

        return ResultHelper.Success(teamsPoints.Adapt<IEnumerable<ChampoionshipTeamPointsResponse>>());
    }

    public async Task<Result<ChampoionshipTeamPointsResponse>> CreateOrUpdateAsync(ChampionshipTeamsPointsRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        if (id > 0)
        {
            await _repository.Update(request.Adapt<Standing>());
        }
        else
        {
            id = await _repository.InsertAndGetId(request.Adapt<Standing>());
        }

        var teamsPoints = await _repository.Select(id);

        return ResultHelper.Success(teamsPoints.Adapt<ChampoionshipTeamPointsResponse>());
    }
}
