using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
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

        cancellationToken.ThrowIfCancellationRequested();

        return ResultHelper.Success(leagues.Adapt<IEnumerable<LeagueResponse>>());
    }

    public async Task<Result<LeagueResponse>> UpdateAsync(int id, LeagueRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<League>();
        entity.Id = id;

        await _repository.Update(entity);

        cancellationToken.ThrowIfCancellationRequested();

        return ResultHelper.Success(entity.Adapt<LeagueResponse>());
    }

    public async Task<Result> UpdateDatabaseAsync(CancellationToken cancellationToken)
    {
        var leagues = await _provider.GetLeagues(new() { Season = DateTime.Now.Year });

        cancellationToken.ThrowIfCancellationRequested();

        var entity = leagues.Select(l => new League
        {
            ExternalId = l.League?.Id ?? 0,
            DataSourceId = 1,
            Name = l.League?.Name,
            Image = l.League?.Logo
        }).OrderBy(x => x.ExternalId);

        await _repository.InsertOrUpdate(entity);

        cancellationToken.ThrowIfCancellationRequested();

        return ResultHelper.Success();
    }

    public async Task<Result<LeagueResponse>> ToggleStatusAsync(int id, bool enabled, CancellationToken cancellationToken)
    {
        var league = await _repository.Select(id);

        cancellationToken.ThrowIfCancellationRequested();

        league.Enabled = enabled;

        await _repository.Update(league);

        cancellationToken.ThrowIfCancellationRequested();

        return ResultHelper.Success(league.Adapt<LeagueResponse>());
    }

    #endregion
}
