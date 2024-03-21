using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;

public class TeamsPointsService : ITeamsPointsService
{
    private readonly IChampionshipTeamPointsRepository _repository;

    public TeamsPointsService(IChampionshipTeamPointsRepository repository)
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
            await _repository.Update(request.Adapt<ChampionshipTeamPoints>());
        }
        else
        {
            id = await _repository.InsertAndGetId(request.Adapt<ChampionshipTeamPoints>());
        }

        var teamsPoints = await _repository.Select(id);

        return ResultHelper.Success(teamsPoints.Adapt<ChampoionshipTeamPointsResponse>());
    }
}
