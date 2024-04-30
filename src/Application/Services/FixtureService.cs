using Mapster;
using Microsoft.Extensions.Options;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.CrossCutting.Settings;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class FixtureService : IFixtureService
{
    #region Fields

    private readonly IFixturesRepository _fixturesRepository;
    private readonly IMatchesRepository _matchesRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly ICacheService _cache;
    private readonly IOptions<FixturesSettings> _options;

    #endregion

    #region Contructors

    public FixtureService(IFixturesRepository fixturesRepository,
                          IMatchesRepository matchesRepository,
                          ITeamsRepository teamsRepository,
                          ICacheService cache,
                          IOptions<FixturesSettings> options)
    {
        _fixturesRepository = fixturesRepository;
        _matchesRepository = matchesRepository;
        _teamsRepository = teamsRepository;
        _cache = cache;
        _options = options;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<FixtureResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var from = DateTime.Now.Date;
        var to = DateTime.Now.Date.AddDays(_options.Value.DaysToSearch + 1).AddTicks(-1);

        var fixtures = await _cache.GetOrCreateAsync(_options.Value.CacheKey ?? "PalpiteFC:Fixtures",
                                                     () => GetFixtures(from, to),
                                                     _options.Value.CacheExpiration,
                                                     cancellationToken: cancellationToken);

        return ResultHelper.Success(fixtures);
    }

    public async Task<Result<FixtureResponse>> CreateOrUpdateAsync(FixtureRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id.GetValueOrDefault();

        if (id > 0)
        {
            await _fixturesRepository.Update(request.Adapt<Fixture>());
        }
        else
        {
            id = await _fixturesRepository.InsertAndGetId(request.Adapt<Fixture>());
        }

        var league = await _fixturesRepository.Select(id);

        return ResultHelper.Success(league.Adapt<FixtureResponse>());
    }

    public async Task<Result<FixtureResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _fixturesRepository.Delete(id);

        return ResultHelper.Success<FixtureResponse>(new() { Id = id });
    }

    #endregion

    #region Non-Public Methods

    private async Task<IEnumerable<FixtureResponse>> GetFixtures(DateTime from, DateTime to)
    {
        var fixtures = await _fixturesRepository.Select(from, to);
        var matches = await _matchesRepository.Select();
        var teams = await _teamsRepository.Select();

        var fixturesResponse = new List<FixtureResponse>();

        foreach (var fixture in fixtures)
        {
            fixturesResponse.Add(new FixtureResponse()
            {
                Id = fixture.Id,
                LeagueId = fixture.LeagueId,
                Name = fixture.Name,
                Start = fixture.Start,
                Finished = fixture.Finished,
                HomeTeam = (teams, matches.Where(w => w.FixtureId == fixture.Id).ElementAt(0)).Adapt<MatchResponse>(),
                AwayTeam = (teams, matches.Where(w => w.FixtureId == fixture.Id).ElementAt(1)).Adapt<MatchResponse>(),
            });
        }

        return fixturesResponse.OrderBy(x => x.Start);
    }

    #endregion
}
