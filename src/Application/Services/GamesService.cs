using Mapster;
using Microsoft.Extensions.Options;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;
using PalpiteFC.Api.Domain.Settings;

namespace PalpiteFC.Api.Application.Services;

public class GamesService : IGamesService
{
    #region Fields

    private readonly IGamesRepository _gamesRepository;
    private readonly ITeamsGamesRepository _teamsGamesRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly ICacheService _cache;
    private readonly IOptions<FixturesSettings> _options;

    #endregion

    #region Contructors

    public GamesService(IGamesRepository gamesRepository,
                        ITeamsGamesRepository teamsGamesRepository,
                        ITeamsRepository teamsRepository,
                        ICacheService cache,
                        IOptions<FixturesSettings> options)
    {
        _gamesRepository = gamesRepository;
        _teamsGamesRepository = teamsGamesRepository;
        _teamsRepository = teamsRepository;
        _cache = cache;
        _options = options;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<GameResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var from = DateTime.Now.ToString("yyyy-MM-dd");
        var to = DateTime.Now.AddDays(_options.Value.DaysToSearch).ToString("yyyy-MM-dd");

        var fixtures = await _cache.GetOrCreateAsync(_options.Value.CacheKey ?? "PalpiteFC:Fixtures",
                                                     () => GetFixtures(from, to),
                                                     _options.Value.CacheExpiration);

        return ResultHelper.Success(fixtures);
    }

    public async Task<Result<GameResponse>> CreateOrUpdateAsync(GameRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id.GetValueOrDefault();

        if (id > 0)
        {
            await _gamesRepository.Update(request.Adapt<Games>());
        }
        else
        {
            id = await _gamesRepository.InsertAndGetId(request.Adapt<Games>());
        }

        var championship = await _gamesRepository.Select(id);

        return ResultHelper.Success(championship.Adapt<GameResponse>());
    }

    public async Task<Result<GameResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _gamesRepository.Delete(id);

        return ResultHelper.Success<GameResponse>(new() { Id = id });
    }

    #endregion

    #region Non-Public Methods

    private async Task<IEnumerable<GameResponse>> GetFixtures(string from, string to)
    {
        var games = await _gamesRepository.Select(from, to);
        var teamsGame = await _teamsGamesRepository.Select();
        var teams = await _teamsRepository.Select();

        var gamesResponse = new List<GameResponse>();

        foreach (var game in games)
        {
            gamesResponse.Add(new GameResponse()
            {
                Id = game.Id,
                ChampionshipId = game.ChampionshipId,
                Name = game.Name,
                Start = game.Start,
                Finished = game.Finished,
                FirstTeam = (teams, teamsGame.Where(w => w.GameId == game.Id).ElementAt(0)).Adapt<TeamGameResponse>(),
                SecondTeam = (teams, teamsGame.Where(w => w.GameId == game.Id).ElementAt(1)).Adapt<TeamGameResponse>(),
            });
        }

        return gamesResponse.OrderBy(x => x.Start);
    }

    #endregion
}
