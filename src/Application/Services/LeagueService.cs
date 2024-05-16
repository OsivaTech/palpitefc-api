using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Integrations.Interfaces;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class LeagueService : ILeagueService
{
    #region Fields

    private readonly ILeaguesRepository _repository;
    private readonly IApiFootballProvider _provider;

    #endregion

    #region Constructor

    public LeagueService(ILeaguesRepository rerpository, IApiFootballProvider provider)
    {
        _repository = rerpository;
        _provider = provider;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<LeagueResponse>>> GetEnabledAsync(CancellationToken cancellationToken)
    {
        var leagues = await _repository.SelectEnabled();

        return ResultHelper.Success(leagues.Adapt<IEnumerable<LeagueResponse>>());
    }

    public async Task<Result> UpdateAsync(CancellationToken cancellationToken)
    {
        var leagues = await _provider.GetLeagues(new() { Season = DateTime.Now.Year });

        var entity = leagues.Select(l => new League
        {
            ExternalId = l.League?.Id ?? 0,
            DataSourceId = 1,
            Name = l.League?.Name,
            Image = l.League?.Logo
        }).OrderBy(x => x.ExternalId);

        await _repository.InsertOrUpdate(entity);

        return ResultHelper.Success();
    }

    public async Task<Result<LeagueResponse>> ToggleStatusAsync(int id, bool enabled, CancellationToken cancellationToken)
    {
        var league = await _repository.Select(id);

        league.Enabled = enabled;

        await _repository.Update(league);

        return ResultHelper.Success(league.Adapt<LeagueResponse>());
    }

    #endregion
}
