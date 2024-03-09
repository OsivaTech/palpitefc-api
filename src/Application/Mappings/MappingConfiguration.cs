using Mapster;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Entities.ApiFootball;
using System.Linq.Expressions;

namespace PalpiteApi.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<(IEnumerable<Options>, Votes), VoteResponse>().MapWith(MapListOptonsAndVotesToVoteResponse());
        config.ForType<(IEnumerable<Games>, Championships), ChampionshipResponse>().MapWith(MapListGamesAndChampionshipsToChampionshipResponse());

        config.ForType<Match, GameResponse>().MapWith(MapMatchToGameResponse());
        config.ForType<Team, TeamGameResponse>().MapWith(MapTeamToTeamGameResponse());
    }

    private Expression<Func<Team, TeamGameResponse>> MapTeamToTeamGameResponse()
    {
        return src => new TeamGameResponse
        {
            TeamId = src.Id.Value,
            Name = src.Name,
            Image = src.Logo
        };
    }

    private Expression<Func<Match, GameResponse>> MapMatchToGameResponse()
    {
        return src => new GameResponse
        {
            Id = src.Fixture.Id.Value,
            Name = "",
            ChampionshipId = src.League.Id.Value,
            FirstTeam = src.Teams.Home.Adapt<TeamGameResponse>(),
            SecondTeam = src.Teams.Away.Adapt<TeamGameResponse>(),
            Start = src.Fixture.Date.Value,
            Finished = src.Fixture.Status.Long.Equals("Match Finished"), 
        };
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
}
