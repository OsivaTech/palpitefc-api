using Mapster;
using Microsoft.Extensions.Caching.Memory;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.ApiFootball;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Interfaces.Integrations;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;

public class GamesService : IGamesService
{
    #region Fields


    private readonly IApiFootballProvider _apiFootballProvider;
    private readonly IGamesRepository _repository;
    private readonly IMemoryCache _cache;

    #endregion

    #region Contructors

    public GamesService(IApiFootballProvider apiFootballProvider, IGamesRepository repository, IMemoryCache cache)
    {
        _apiFootballProvider = apiFootballProvider;
        _repository = repository;
        _cache = cache;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<GameResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        string[] champsIds = ["11", "13", "73", "475", "624", "629"];
        var tasks = champsIds.Select(GetMatches);
        var matchesArray = await Task.WhenAll(tasks);

        var matchesJoined = matchesArray.Where(x => x is not null).SelectMany(x => x);
        var matches = matchesJoined.Adapt<IEnumerable<GameResponse>>().ToArray();

        foreach (var match in matches)
        {
            var correspondingMatch = matchesJoined.First(w => w.Fixture!.Id == match.Id);

            match.FirstTeam!.GameId = match.Id;
            match.SecondTeam!.GameId = match.Id;
            match.FirstTeam.Gol = correspondingMatch.Goals!.Home.GetValueOrDefault(0);
            match.SecondTeam.Gol = correspondingMatch.Goals.Away.GetValueOrDefault(0);
        }

        var response = matches.OrderBy(x => x.Start);

        return ResultHelper.Success<IEnumerable<GameResponse>>(response);
    }

    public async Task<Result<GameResponse>> CreateOrUpdateAsync(GameRequest request, CancellationToken cancellationToken)
    {
        var id = request.Id.GetValueOrDefault();

        if (id > 0)
        {
            await _repository.Update(request.Adapt<Games>());
        }
        else
        {
            id = await _repository.InsertAndGetId(request.Adapt<Games>());
        }

        var championship = await _repository.Select(id);

        return ResultHelper.Success(championship.Adapt<GameResponse>());
    }

    public async Task<Result<GameResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _repository.Delete(id);

        return ResultHelper.Success<GameResponse>(new() { Id = id });
    }

    #endregion

    #region Non-Public Methods

    private async Task<IEnumerable<Match>> GetMatches(string champId)
    {
        return await _cache.GetOrCreateAsync(champId, CreateFunc);

        async Task<IEnumerable<Match>> CreateFunc(ICacheEntry entry)
        {
            var from = DateTime.UtcNow.ToString("yyyy-MM-dd");
            var to = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd");

            entry.AbsoluteExpiration = DateTime.Parse(to).ToUniversalTime();

            return await _apiFootballProvider.GetMatchesByLeagueId(champId, from, to);
        }
    }

    #endregion
}
