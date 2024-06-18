using Mapster;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Libraries.DataContracts.MessageTypes;
using System.Linq.Expressions;
using Database = PalpiteFC.Libraries.Persistence.Abstractions.Entities;


namespace PalpiteFC.Api.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Database.Guess, GuessResponse>().MapWith(MapGuessResponseToGuessEntity());
        config.ForType<Database.Fixture, FixtureResponse>().MapWith(MapFixtureEntityToFixtureResponse());
        config.ForType<Database.News, NewsResponse>().MapWith(MapNewsEntityToNewsResponse());
        config.ForType<(Database.Poll, IEnumerable<Database.Option>), PollResponse>().MapWith(MapPollAndListOfOptionsToPollResponse());
        config.ForType<GuessRequest, Database.Guess>().MapWith(MapGuessRequestToGuessEntity());
        config.ForType<StandingRequest, Database.Standing>().MapWith(MapStandingRequestToStandingEntity());
        config.ForType<GuessRequest, GuessMessage>().MapWith(MapGuessRequestToGuessMessage());
    }

    private Expression<Func<Database.News, NewsResponse>> MapNewsEntityToNewsResponse()
    {
        return src => new NewsResponse
        {
            Id = src.Id,
            UserId = src.UserId.GetValueOrDefault(),
            Title = src.Title,
            Content = src.Content,
            Info = src.Info,
            Thumbnail = src.Thumbnail,
            Author = src.User.Adapt<AuthorInfo>()
        };
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
            HomeTeam = new() { Id = src.Match!.HomeId, Goals = src.Match.HomeGoals, Name = src.Match.Home!.Name, Image = src.Match.Home.Image },
            AwayTeam = new() { Id = src.Match!.AwayId, Goals = src.Match.AwayGoals, Name = src.Match.Away!.Name, Image = src.Match.Away.Image },
        };
    }

    private Expression<Func<Database.Guess, GuessResponse>> MapGuessResponseToGuessEntity()
    {
        return src => new GuessResponse
        {
            Id = src.Id,
            FixtureId = src.FixtureId,
            UserId = src.UserId,
            HomeTeam = new() { Id = src.HomeId, Goals = src.HomeGoals },
            AwayTeam = new() { Id = src.AwayId, Goals = src.AwayGoals }
        };
    }

    private static Expression<Func<GuessRequest, GuessMessage>> MapGuessRequestToGuessMessage()
    {
        return src => new GuessMessage
        {
            FixtureId = src.FixtureId,
            HomeId = src.HomeTeam!.Id,
            HomeGoals = src.HomeTeam.Goals,
            AwayId = src.AwayTeam!.Id,
            AwayGoals = src.AwayTeam.Goals
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

    private static Expression<Func<GuessRequest, Database.Guess>> MapGuessRequestToGuessEntity()
    {
        return src => new Database.Guess
        {
            FixtureId = src.FixtureId,
            HomeId = src.HomeTeam.Id,
            HomeGoals = src.HomeTeam.Goals,
            AwayGoals = src.AwayTeam.Goals,
            AwayId = src.AwayTeam.Id,
            CreatedAt = DateTime.UtcNow
        };
    }
}
