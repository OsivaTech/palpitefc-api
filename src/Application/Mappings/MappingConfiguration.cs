using Mapster;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using System.Linq.Expressions;
using ApiFootball = PalpiteFC.Api.Integrations.ApiFootball.Responses;
using Database = PalpiteFC.Libraries.Persistence.Abstractions.Entities;


namespace PalpiteFC.Api.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<ApiFootball.Match, GameResponse>().MapWith(MapMatchToGameResponse());
        config.ForType<ApiFootball.Team, TeamGameResponse>().MapWith(MapTeamToTeamGameResponse());
        config.ForType<(Database.News, Database.User), NewsResponse>().MapWith(MapNewsAndUsersToNewsResponse());
        config.ForType<PalpitationRequest, Database.Guess>().MapWith(MapPalpitationRequestToPalpitations());
        config.ForType<(Database.Poll, IEnumerable<Database.Option>), VoteResponse>().MapWith(MapVotesAndListOfOptionsToVoteResponse());
        config.ForType<ChampionshipTeamsPointsRequest, Database.Standing>().MapWith(MapChampionshipTeamsPointsRequestToChampoionshipTeamPoints());
        config.ForType<(IEnumerable<Database.Team>, Database.Match), TeamGameResponse>().MapWith(MapListTeamsAndTeamsGameToTeamGameResponse());
    }

    private static Expression<Func<(IEnumerable<Database.Team>, Database.Match), TeamGameResponse>> MapListTeamsAndTeamsGameToTeamGameResponse()
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

    private Expression<Func<ChampionshipTeamsPointsRequest, Database.Standing>> MapChampionshipTeamsPointsRequestToChampoionshipTeamPoints()
    {
        return src => new Database.Standing
        {
            Id = src.Id,
            ChampionshipsId = src.ChampionshipId,
            Points = src.Points,
            Position = src.Position.ToString(),
            TeamId = src.TeamId
        };
    }

    private Expression<Func<(Database.Poll, IEnumerable<Database.Option>), VoteResponse>> MapVotesAndListOfOptionsToVoteResponse()
    {
        return src => new VoteResponse
        {
            Id = src.Item1.Id,
            Title = src.Item1.Title,
            Options = src.Item2.Adapt<IEnumerable<OptionsResponse>>()
        };
    }

    private Expression<Func<(Database.News, Database.User), NewsResponse>> MapNewsAndUsersToNewsResponse()
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

    private Expression<Func<PalpitationRequest, Database.Guess>> MapPalpitationRequestToPalpitations()
    {
        return src => new Database.Guess
        {
            GameId = src.GameId,
            FirstTeamId = src.FirstTeam.Id,
            FirstTeamGol = src.FirstTeam.Gol,
            SecondTeamGol = src.SecondTeam.Gol,
            SecondTeamId = src.SecondTeam.Id,
            CreatedAt = DateTime.UtcNow
        };
    }

    private Expression<Func<ApiFootball.Team, TeamGameResponse>> MapTeamToTeamGameResponse()
    {
        return src => new TeamGameResponse
        {
            TeamId = src.Id.Value,
            Name = src.Name,
            Image = src.Logo
        };
    }

    private Expression<Func<ApiFootball.Match, GameResponse>> MapMatchToGameResponse()
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
