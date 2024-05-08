using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class RankingService : IRankingService
{
    #region Fields

    private readonly IUserPointsRepository _repository;
    private readonly UserContext _userContext;

    #endregion

    #region Constructors

    public RankingService(IUserPointsRepository repository, UserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<RankingResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var ranking = new List<RankingResponse>();

        var points = await _repository.Select(DateTime.Parse($"{DateTime.Now.Year}-01-01"), DateTime.Parse($"{DateTime.Now.Year}-12-31").AddDays(1).AddTicks(-1));

        AddRankings(ranking, points.GroupBy(w => w.Fixture?.LeagueId), RankingType.League);
        AddRankings(ranking, points.GroupBy(w => w.Fixture?.Start.Month), RankingType.Month);
        AddRankings(ranking, points.GroupBy(w => w.Fixture?.Start.Year), RankingType.Year);

        return ResultHelper.Success<IEnumerable<RankingResponse>>(ranking);
    }

    public async Task<Result<IEnumerable<RankingResponse>>> GetMockAsync(CancellationToken cancellationToken)
    {
        var response = new List<RankingResponse> {
        new() {
            Type = RankingType.League,
            Info = new RankingInfo
            {
                LeagueId = 13,
            },
            YourPlace = new() { Id = 95, Name = "Eduardo", Points = 750, Place = 11 },
            Placings = new List<UserPlacing> {
                new() { Id = 87, Name = "João", Points = 990, Place = 1 },
                new() { Id = 26, Name = "Maria", Points = 880, Place = 2 },
                new() { Id = 45, Name = "Ana", Points = 860, Place = 3 },
                new() { Id = 32, Name = "Pedro", Points = 820, Place = 4 },
                new() { Id = 98, Name = "Lucas", Points = 810, Place = 5 },
                new() { Id = 11, Name = "Marta", Points = 800, Place = 6 },
                new() { Id = 56, Name = "Luiza", Points = 790, Place = 7 },
                new() { Id = 39, Name = "Gabriel", Points = 780, Place = 8 },
                new() { Id = 72, Name = "Rafael", Points = 770, Place = 9 },
                new() { Id = 84, Name = "Fernanda", Points = 760, Place = 10 },
                new() { Id = 95, Name = "Eduardo", Points = 750, Place = 11 },
                new() { Id = 24, Name = "Amanda", Points = 740, Place = 12 },
                new() { Id = 57, Name = "Bruno", Points = 730, Place = 13 },
                new() { Id = 68, Name = "Carolina", Points = 720, Place = 14 },
                new() { Id = 79, Name = "Rodrigo", Points = 710, Place = 15 },
                new() { Id = 90, Name = "Patricia", Points = 700, Place = 16 },
                new() { Id = 14, Name = "Marcos", Points = 690, Place = 17 },
                new() { Id = 25, Name = "Leticia", Points = 680, Place = 18 },
                new() { Id = 36, Name = "Alexandre", Points = 670, Place = 19 },
                new() { Id = 47, Name = "Gabriela", Points = 660, Place = 20 }
            }
        },
        new() {
            Type = RankingType.Year,
            Info = new RankingInfo
            {
                Year = 2024,
            },
            YourPlace = new() { Id = 95, Name = "Eduardo", Points = 750, Place = 11 },
            Placings =new List<UserPlacing> {
                new() { Id = 87, Name = "João", Points = 990, Place = 1 },
                new() { Id = 26, Name = "Maria", Points = 880, Place = 2 },
                new() { Id = 45, Name = "Ana", Points = 860, Place = 3 },
                new() { Id = 32, Name = "Pedro", Points = 820, Place = 4 },
                new() { Id = 98, Name = "Lucas", Points = 810, Place = 5 },
                new() { Id = 11, Name = "Marta", Points = 800, Place = 6 },
                new() { Id = 56, Name = "Luiza", Points = 790, Place = 7 },
                new() { Id = 39, Name = "Gabriel", Points = 780, Place = 8 },
                new() { Id = 72, Name = "Rafael", Points = 770, Place = 9 },
                new() { Id = 84, Name = "Fernanda", Points = 760, Place = 10 },
                new() { Id = 95, Name = "Eduardo", Points = 750, Place = 11 },
                new() { Id = 24, Name = "Amanda", Points = 740, Place = 12 },
                new() { Id = 57, Name = "Bruno", Points = 730, Place = 13 },
                new() { Id = 68, Name = "Carolina", Points = 720, Place = 14 },
                new() { Id = 79, Name = "Rodrigo", Points = 710, Place = 15 },
                new() { Id = 90, Name = "Patricia", Points = 700, Place = 16 },
                new() { Id = 14, Name = "Marcos", Points = 690, Place = 17 },
                new() { Id = 25, Name = "Leticia", Points = 680, Place = 18 },
                new() { Id = 36, Name = "Alexandre", Points = 670, Place = 19 },
                new() { Id = 47, Name = "Gabriela", Points = 660, Place = 20 }
            }
        },
        new() {
            Type = RankingType.Month,
            Info = new RankingInfo
            {
                Month = 1,
                Year = 2024,
            },
            YourPlace = new() { Id = 95, Name = "Eduardo", Points = 750, Place = 11 },
            Placings = new List<UserPlacing> {
                new() { Id = 21, Name = "Guilherme", Points = 850, Place = 1 },
                new() { Id = 22, Name = "Isabela", Points = 840, Place = 2 },
                new() { Id = 73, Name = "Felipe", Points = 830, Place = 3 },
                new() { Id = 94, Name = "Carolina", Points = 820, Place = 4 },
                new() { Id = 35, Name = "Rodrigo", Points = 810, Place = 5 },
                new() { Id = 66, Name = "Patricia", Points = 800, Place = 6 },
                new() { Id = 77, Name = "Marcos", Points = 790, Place = 7 },
                new() { Id = 28, Name = "Leticia", Points = 780, Place = 8 },
                new() { Id = 49, Name = "Alexandre", Points = 770, Place = 9 },
                new() { Id = 30, Name = "Gabriela", Points = 760, Place = 10 },
                new() { Id = 95, Name = "Eduardo", Points = 750, Place = 11 },
                new() { Id = 24, Name = "Amanda", Points = 740, Place = 12 },
                new() { Id = 57, Name = "Bruno", Points = 730, Place = 13 },
                new() { Id = 68, Name = "Carolina", Points = 720, Place = 14 },
                new() { Id = 79, Name = "Rodrigo", Points = 710, Place = 15 },
                new() { Id = 90, Name = "Patricia", Points = 700, Place = 16 },
                new() { Id = 14, Name = "Marcos", Points = 690, Place = 17 },
                new() { Id = 25, Name = "Leticia", Points = 680, Place = 18 },
                new() { Id = 36, Name = "Alexandre", Points = 670, Place = 19 },
                new() { Id = 47, Name = "Gabriela", Points = 660, Place = 20 }
            }
        },
        new() {
            Type = RankingType.Month,
            Info = new RankingInfo
            {
                Month = 2,
                Year = 2024,
            },
            YourPlace = new() { Id = 95, Name = "Eduardo", Points = 750, Place = 9 },
            Placings = new List<UserPlacing> {
                new() { Id = 31, Name = "Leonardo", Points = 750, Place = 1 },
                new() { Id = 92, Name = "Amanda", Points = 740, Place = 2 },
                new() { Id = 53, Name = "Gustavo", Points = 730, Place = 3 },
                new() { Id = 64, Name = "Fernanda", Points = 720, Place = 4 },
                new() { Id = 75, Name = "Henrique", Points = 710, Place = 5 },
                new() { Id = 86, Name = "Juliana", Points = 700, Place = 6 },
                new() { Id = 97, Name = "Diego", Points = 690, Place = 7 },
                new() { Id = 18, Name = "Mariana", Points = 680, Place = 8 },
                new() { Id = 79, Name = "Eduardo", Points = 670, Place = 9 },
                new() { Id = 40, Name = "Priscila", Points = 660, Place = 10 },
                new() { Id = 95, Name = "Eduardo", Points = 650, Place = 11 },
                new() { Id = 24, Name = "Amanda", Points = 640, Place = 12 },
                new() { Id = 57, Name = "Bruno", Points = 630, Place = 13 },
                new() { Id = 68, Name = "Carolina", Points = 620, Place = 14 },
                new() { Id = 79, Name = "Rodrigo", Points = 610, Place = 15 },
                new() { Id = 90, Name = "Patricia", Points = 600, Place = 16 },
                new() { Id = 14, Name = "Marcos", Points = 590, Place = 17 },
                new() { Id = 25, Name = "Leticia", Points = 580, Place = 18 },
                new() { Id = 36, Name = "Alexandre", Points = 570, Place = 19 },
                new() { Id = 47, Name = "Gabriela", Points = 560, Place = 20 }
            }
        }
    };

        return await Task.FromResult(ResultHelper.Success(response.AsEnumerable()));
    }

    #endregion

    #region Non-Public Methods

    private void AddRankings(List<RankingResponse> ranking, IEnumerable<IGrouping<int?, UserPoint>> groupedPoints, RankingType type)
    {
        foreach (var group in groupedPoints)
        {
            var rankingResponse = new RankingResponse()
            {
                Type = type,
                Placings = CreatePlacing(group),
                Info = type switch
                {
                    RankingType.League => new RankingInfo()
                    {
                        LeagueId = group.Key,
                    },
                    RankingType.Month => new RankingInfo()
                    {
                        Month = group.Key,
                        Year = group.FirstOrDefault()?.Fixture?.Start.Year,
                    },
                    RankingType.Year => new RankingInfo()
                    {
                        Year = group.Key ?? 0,
                    },
                    _ => default
                }
            };

            if (_userContext.Id > 0)
            {
                rankingResponse.YourPlace = rankingResponse.Placings.FirstOrDefault(w => w.Id == _userContext.Id);
            }

            ranking.Add(rankingResponse);
        }
    }

    private static IEnumerable<UserPlacing> CreatePlacing(IGrouping<int?, UserPoint> leaguePoints)
    {
        return leaguePoints.GroupBy(up => new { up.User?.Id, up.User?.Name })
                           .Select(g => new UserPlacing
                           {
                               Id = g.Key.Id ?? 0,
                               Name = g.Key.Name,
                               Points = g.Sum(up => up.Points),
                               Guesses = g.Count()
                           })
                           .OrderByDescending(x => x.Points)
                           .ThenBy(x => x.Guesses)
                           .Select((x, index) => { x.Place = index + 1; return x; });
    }

    #endregion
}