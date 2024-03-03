using Mapster;
using MediatR;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Queries.Handlers;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GameResponse>>
{
    #region Fields

    private readonly IGamesRepository _gamesRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly ITeamsGamesRepository _teamsGameRepository;

    #endregion

    #region Contructors

    public GetGamesQueryHandler(IGamesRepository gameRepository, ITeamsRepository teamsRepository, ITeamsGamesRepository teamsGameRepository)
    {
        _gamesRepository = gameRepository;
        _teamsRepository = teamsRepository;
        _teamsGameRepository = teamsGameRepository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<GameResponse>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _gamesRepository.Select();
        var teamsGame = await _teamsGameRepository.Select();
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
                FirstTeam = (teams, teamsGame.Where(w => w.GameId == game.Id).ElementAt(0)).Adapt<TeamGameResponse>(),
                SecondTeam = (teams, teamsGame.Where(w => w.GameId == game.Id).ElementAt(1)).Adapt<TeamGameResponse>(),
            });
        }

        return gamesResponse;
    }

    #endregion
}
