using Mapster;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamsRepository _repository;

    public TeamService(ITeamsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<TeamResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var teams = await _repository.Select();

        return ResultHelper.Success(teams.Adapt<IEnumerable<TeamResponse>>());
    }
}
