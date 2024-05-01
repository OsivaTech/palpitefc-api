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
        config.ForType<Database.Guess, GuessResponse>().MapWith(MapGuessResponseToGuessEntity());
        config.ForType<Database.User, UserResponse>().MapWith(MapUserEntityToUserResponse());
        config.ForType<Database.Fixture, FixtureResponse>().MapWith(MapFixtureEntityToFixtureResponse());
        config.ForType<(Database.News, Database.User), NewsResponse>().MapWith(MapNewsAndUsersToNewsResponse());
        config.ForType<(Database.Poll, IEnumerable<Database.Option>), PollResponse>().MapWith(MapPollAndListOfOptionsToPollResponse());
        config.ForType<GuessRequest, Database.Guess>().MapWith(MapGuessRequestToGuessEntity());
        config.ForType<StandingRequest, Database.Standing>().MapWith(MapStandingRequestToStandingEntity());
        config.ForType<GuessRequest, GuessMessage>().MapWith(MapGuessRequestToGuessMessage());
    }

    private Expression<Func<Database.Fixture, FixtureResponse>> MapFixtureEntityToFixtureResponse()
    {
        return src => new FixtureResponse
        {
            Id = src.Id,
            LeagueId = src.LeagueId,
            Name = src.Name,
            Start = src.Start,
            Finished = src.Finished,
            HomeTeam = new() { Id = src.Match!.Id, FixtureId = src.Id, TeamId = src.Match!.HomeId, Goals = src.Match.HomeGoals, Name = src.Match.HomeTeam!.Name, Image = src.Match.HomeTeam.Image },
            AwayTeam = new() { Id = src.Match!.Id, FixtureId = src.Id, TeamId = src.Match!.AwayId, Goals = src.Match.AwayGoals, Name = src.Match.AwayTeam!.Name, Image = src.Match.AwayTeam.Image },
        };
    }

    private Expression<Func<Database.User, UserResponse>> MapUserEntityToUserResponse()
    {
        return src => new UserResponse
        {
            Id = src.Id,
            Name = src.Name,
            Birthday = src.Birthday,
            Email = src.Email,
            Info = src.Info,
            PhoneNumber = src.Number,
            Points = src.Points,
            Role = src.Role,
            Team = src.Team
        };
    }

    private Expression<Func<Database.Guess, GuessResponse>> MapGuessResponseToGuessEntity()
    {
        return src => new GuessResponse
        {
            Id = src.Id,
            FixtureId = src.FixtureId,
            UserId = src.UserId,
            HomeTeam = new() { Id = src.HomeTeamId, Goals = src.HomeTeamGoals },
            AwayTeam = new() { Id = src.AwayTeamId, Goals = src.AwayTeamGoals }
        };
    }

    private static Expression<Func<GuessRequest, GuessMessage>> MapGuessRequestToGuessMessage()
    {
        return src => new GuessMessage
        {
            FixtureId = src.FixtureId,
            HomeTeamId = src.HomeTeam!.Id,
            HomeTeamGoals = src.HomeTeam.Goals,
            AwayTeamId = src.AwayTeam!.Id,
            AwayTeamGoals = src.AwayTeam.Goals
        };
    }

    private static Expression<Func<StandingRequest, Database.Standing>> MapStandingRequestToStandingEntity()
    {
        return src => new Database.Standing
        {
            Id = src.Id,
            LeagueId = src.LeagueId,
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
            FixtureId = src.FixtureId,
            HomeTeamId = src.HomeTeam.Id,
            HomeTeamGoals = src.HomeTeam.Goals,
            AwayTeamGoals = src.AwayTeam.Goals,
            AwayTeamId = src.AwayTeam.Id,
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
