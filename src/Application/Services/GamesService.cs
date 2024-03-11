using Mapster;
using Microsoft.Extensions.Caching.Memory;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Interfaces.Integrations;

namespace PalpiteApi.Application.Services;

public class GamesService : IGamesService
{
    #region Fields


    private readonly IApiFootballProvider _apiFootballProvider;
    private readonly IMemoryCache _cache;

    #endregion

    #region Contructors

    public GamesService(IApiFootballProvider apiFootballProvider, IMemoryCache cache)
    {
        _apiFootballProvider = apiFootballProvider;
        _cache = cache;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<GameResponse>> GetAsync(CancellationToken cancellationToken)
    {
        string[] champsIds = ["11", "13", "73", "475", "624", "629"];

        var games = Enumerable.Empty<GameResponse>();

        foreach (var champId in champsIds)
        {
            var cachedGames = await _cache.GetOrCreate(champId, async entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1);

                return await _apiFootballProvider.GetMatchesByLeagueId(champId,
                                                                       DateTime.UtcNow.ToString("yyyy-MM-dd"),
                                                                       DateTime.UtcNow.AddDays(14).ToString("yyyy-MM-dd"));
            })!;

            var adaptedGame = cachedGames.Adapt<IEnumerable<GameResponse>>().ToArray();

            for (int i = 0; i < adaptedGame.Length; i++)
            {
                var item = adaptedGame.ElementAt(i);

                adaptedGame[i].FirstTeam.GameId = item.Id;
                adaptedGame[i].SecondTeam.GameId = item.Id;
                adaptedGame[i].FirstTeam.Gol = cachedGames.First(w => w.Fixture.Id.Value == item.Id).Goals.Home.GetValueOrDefault(0);
                adaptedGame[i].SecondTeam.Gol = cachedGames.First(w => w.Fixture.Id.Value == item.Id).Goals.Away.GetValueOrDefault(0);
            }

            games = games.Concat(adaptedGame);
        }

        var response = games.Adapt<IEnumerable<GameResponse>>();

        return response.OrderBy(x => x.Start);
    }

    #endregion
}
