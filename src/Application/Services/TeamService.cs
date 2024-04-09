using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

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
