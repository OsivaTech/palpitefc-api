using Mapster;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.DataContracts.MessageTypes;
using System.Linq.Expressions;
using ApiFootball = PalpiteFC.Api.Integrations.ApiFootball.Responses;
using Database = PalpiteFC.Libraries.Persistence.Abstractions.Entities;


namespace PalpiteFC.Api.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<ApiFootball.Match, FixtureResponse>().MapWith(MapMatchToFixtureResponse());
        config.ForType<ApiFootball.Team, MatchResponse>().MapWith(MapTeamToMatchResponse());
        config.ForType<(Database.News, Database.User), NewsResponse>().MapWith(MapNewsAndUsersToNewsResponse());
        config.ForType<GuessRequest, Database.Guess>().MapWith(MapGuessRequestToGuessEntity());
        config.ForType<Database.Guess, GuessResponse>().MapWith(MapGuessResponseToGuessEntity());
        config.ForType<(Database.Poll, IEnumerable<Database.Option>), PollResponse>().MapWith(MapPollAndListOfOptionsToPollResponse());
        config.ForType<StandingRequest, Database.Standing>().MapWith(MapStandingRequestToStandingEntity());
        config.ForType<(IEnumerable<Database.Team>, Database.Match), MatchResponse>().MapWith(MapListTeamsAndMatchToMatchResponse());
        config.ForType<GuessRequest, GuessMessage>().MapWith(MapGuessRequestToGuessMessage());
    }

    private Expression<Func<Database.Guess, GuessResponse>> MapGuessResponseToGuessEntity()
    {
        return src => new GuessResponse
        {
            Id = src.Id,
            FixtureId = src.GameId,
            UserId = src.UserId,
            HomeTeamId = src.FirstTeamId,
            HomeTeamGoal = src.FirstTeamGol,
            AwayTeamId = src.SecondTeamId,
            AwayTeamGoal = src.SecondTeamGol
        };
    }

    private static Expression<Func<GuessRequest, GuessMessage>> MapGuessRequestToGuessMessage()
    {
        return src => new GuessMessage
        {
            GameId = src.FixtureId,
            FirstTeamId = src.HomeTeam!.Id,
            FirstTeamGol = src.HomeTeam.Goal,
            SecondTeamId = src.AwayTeam!.Id,
            SecondTeamGol = src.AwayTeam.Goal
        };
    }

    private static Expression<Func<(IEnumerable<Database.Team>, Database.Match), MatchResponse>> MapListTeamsAndMatchToMatchResponse()
    {
        return src => new MatchResponse
        {
            FixtureId = src.Item2.GameId,
            Goal = src.Item2.Gol,
            Id = src.Item2.Id,
            TeamId = src.Item2.TeamId,
            Image = src.Item1.First(w => w.Id == src.Item2.TeamId).Image,
            Name = src.Item1.First(w => w.Id == src.Item2.TeamId).Name
        };
    }

    private static Expression<Func<StandingRequest, Database.Standing>> MapStandingRequestToStandingEntity()
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

    private static Expression<Func<(Database.Poll, IEnumerable<Database.Option>), PollResponse>> MapPollAndListOfOptionsToPollResponse()
    {
        return src => new PollResponse
        {
            Id = src.Item1.Id,
            Title = src.Item1.Title,
            Options = src.Item2.Adapt<IEnumerable<OptionsResponse>>()
        };
    }

    private static Expression<Func<(Database.News, Database.User), NewsResponse>> MapNewsAndUsersToNewsResponse()
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

    private static Expression<Func<GuessRequest, Database.Guess>> MapGuessRequestToGuessEntity()
    {
        return src => new Database.Guess
        {
            GameId = src.FixtureId,
            FirstTeamId = src.HomeTeam.Id,
            FirstTeamGol = src.HomeTeam.Goal,
            SecondTeamGol = src.AwayTeam.Goal,
            SecondTeamId = src.AwayTeam.Id,
            CreatedAt = DateTime.UtcNow
        };
    }

    private static Expression<Func<ApiFootball.Team, MatchResponse>> MapTeamToMatchResponse()
    {
        return src => new MatchResponse
        {
            TeamId = src.Id.Value,
            Name = src.Name,
            Image = src.Logo
        };
    }

    private static Expression<Func<ApiFootball.Match, FixtureResponse>> MapMatchToFixtureResponse()
    {
        return src => new FixtureResponse
        {
            Id = src.Fixture.Id.Value,
            Name = "",
            LeagueId = src.League.Id.Value,
            HomeTeam = src.Teams.Home.Adapt<MatchResponse>(),
            AwayTeam = src.Teams.Away.Adapt<MatchResponse>(),
            Start = src.Fixture.Date.Value,
            Finished = src.Fixture.Status.Long.Equals("Match Finished")
        };
    }
}
