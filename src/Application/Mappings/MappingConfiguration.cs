using Mapster;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities.ApiFootball;
using PalpiteApi.Domain.Entities.Database;
using System.Linq.Expressions;

namespace PalpiteApi.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Match, GameResponse>().MapWith(MapMatchToGameResponse());
        config.ForType<Team, TeamGameResponse>().MapWith(MapTeamToTeamGameResponse());
        config.ForType<(News, Users), NewsResponse>().MapWith(MapNewsAndUsersToNewsResponse());
        config.ForType<PalpitationRequest, Palpitations>().MapWith(MapPalpitationRequestToPalpitations());
        config.ForType<(Votes, IEnumerable<Options>), VoteResponse>().MapWith(MapVotesAndListOfOptionsToVoteResponse());
    }

    private Expression<Func<(Votes, IEnumerable<Options>), VoteResponse>> MapVotesAndListOfOptionsToVoteResponse()
    {
        return src => new VoteResponse
        {
            Id = src.Item1.Id,
            Title = src.Item1.Title,
            Options = src.Item2.Adapt<IEnumerable<OptionsResponse>>()
        };
    }

    private Expression<Func<(News, Users), NewsResponse>> MapNewsAndUsersToNewsResponse()
    {
        return src => new NewsResponse
        {
            Id = src.Item1.Id,
            Title = src.Item1.Title,
            Info = src.Item1.Info,
            Content = src.Item1.Content,
            UserId = src.Item1.UserId,
            Author = new()
            {
                Id = src.Item2.Id,
                Name = src.Item2.Name,
                Team = src.Item2.Team,
            }
        };
    }

    private Expression<Func<PalpitationRequest, Palpitations>> MapPalpitationRequestToPalpitations()
    {
        return src => new Palpitations
        {
            GameId = src.GameId,
            FirstTeamId = src.FirstTeam.Id,
            FirstTeamGol = src.FirstTeam.Gol,
            SecondTeamGol = src.SecondTeam.Gol,
            SecondTeamId = src.SecondTeam.Id,
            CreatedAt = DateTime.UtcNow
        };
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
}
