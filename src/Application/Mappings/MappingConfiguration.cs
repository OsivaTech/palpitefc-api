﻿using Mapster;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.ApiFootball;
using PalpiteFC.Api.Domain.Entities.Database;
using System.Linq.Expressions;

namespace PalpiteFC.Api.Application.Mappings;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Match, GameResponse>().MapWith(MapMatchToGameResponse());
        config.ForType<Domain.Entities.ApiFootball.Team, TeamGameResponse>().MapWith(MapTeamToTeamGameResponse());
        config.ForType<(News, Users), NewsResponse>().MapWith(MapNewsAndUsersToNewsResponse());
        config.ForType<PalpitationRequest, Palpitations>().MapWith(MapPalpitationRequestToPalpitations());
        config.ForType<(Votes, IEnumerable<Options>), VoteResponse>().MapWith(MapVotesAndListOfOptionsToVoteResponse());
        config.ForType<ChampionshipTeamsPointsRequest, ChampionshipTeamPoints>().MapWith(MapChampionshipTeamsPointsRequestToChampoionshipTeamPoints());
        config.ForType<(IEnumerable<Teams>, TeamsGame), TeamGameResponse>().MapWith(MapListTeamsAndTeamsGameToTeamGameResponse());
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

    private Expression<Func<ChampionshipTeamsPointsRequest, ChampionshipTeamPoints>> MapChampionshipTeamsPointsRequestToChampoionshipTeamPoints()
    {
        return src => new ChampionshipTeamPoints
        {
            Id = src.Id,
            ChampionshipsId = src.ChampionshipId,
            Points = src.Points,
            Position = src.Position.ToString(),
            TeamId = src.TeamId
        };
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

    private Expression<Func<Domain.Entities.ApiFootball.Team, TeamGameResponse>> MapTeamToTeamGameResponse()
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
