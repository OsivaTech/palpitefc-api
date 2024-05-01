using Mapster;
using Microsoft.Extensions.Options;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.CrossCutting.Settings;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class FixtureService : IFixtureService
{
    #region Fields

    private readonly IFixturesRepository _fixturesRepository;
    private readonly ICacheService _cache;
    private readonly IOptions<FixturesSettings> _options;

    #endregion

    #region Contructors

    public FixtureService(IFixturesRepository fixturesRepository,
                          ICacheService cache,
                          IOptions<FixturesSettings> options)
    {
        _fixturesRepository = fixturesRepository;
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

    #endregion

    #region Non-Public Methods

    private async Task<IEnumerable<FixtureResponse>> GetFixtures(DateTime from, DateTime to)
    {
        var fixtures = await _fixturesRepository.Select(from, to);

        var fixturesResponse = fixtures.Adapt<IEnumerable<FixtureResponse>>();

        return fixturesResponse.OrderBy(x => x.Start);
    }

    #endregion
}
