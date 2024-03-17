using Mapster;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services;

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
