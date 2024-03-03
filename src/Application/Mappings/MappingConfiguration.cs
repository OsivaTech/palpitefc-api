using Mapster;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities;
using System.Linq.Expressions;

namespace PalpiteApi.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<(IEnumerable<Options>, Votes), VoteResponse>().MapWith(MapListOptonsAndVotesToVoteResponse());
        config.ForType<(IEnumerable<Games>, Championships), ChampionshipResponse>().MapWith(MapListGamesAndChampionshipsToChampionshipResponse());
        config.ForType<(IEnumerable<Teams>, TeamsGame), TeamGameResponse>().MapWith(MapListTeamsAndTeamsGameToTeamGameResponse());
    }

    private Expression<Func<(IEnumerable<Options>, Votes), VoteResponse>> MapListOptonsAndVotesToVoteResponse()
    {
        return src => new VoteResponse
        {
            Id = src.Item2.Id,
            Title = src.Item2.Title,
            Options = src.Item1.Adapt<IEnumerable<OptionsResponse>>()
        };
    }

    private Expression<Func<(IEnumerable<Games>, Championships), ChampionshipResponse>> MapListGamesAndChampionshipsToChampionshipResponse()

    {
        return src => new ChampionshipResponse
        {
            Id = src.Item2.Id,
            Name = src.Item2.Name,
            Games = src.Item1.Adapt<IEnumerable<GameResponse>>()
        };
    }

    private static Expression<Func<(IEnumerable<Teams>, TeamsGame), TeamGameResponse>> MapListTeamsAndTeamsGameToTeamGameResponse()
    {
        return src => new TeamGameResponse
        {
            GameId = src.Item2.GameId,
            Gol = src.Item2.Gol,
            Id = src.Item2.Id,
            TeamId = src.Item2.TeamId,
            Image = src.Item1.First(w => w.Id == src.Item2.TeamId).Image,
            Name = src.Item1.First(w => w.Id == src.Item2.TeamId).Name
        };
    }
}
