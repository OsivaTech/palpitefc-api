using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Errors;
using PalpiteFC.Api.Domain.Interfaces;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;

public class RankingService : IRankingService
{
    private readonly IUserPointsRepository _userPointsRepository;
    private readonly IPointSeasonsRepository _pointSeasonRepository;

    public RankingService(IUserPointsRepository repository, IPointSeasonsRepository pointSeasonRepository)
    {
        _userPointsRepository = repository;
        _pointSeasonRepository = pointSeasonRepository;
    }

    public async Task<Result<IEnumerable<RankingResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var pointSeason = await _pointSeasonRepository.SelectCurrent();

        if (pointSeason is null)
        {
            return ResultHelper.Failure<IEnumerable<RankingResponse>>(PointSeasonsErrors.PointSeasonNotFound);
        }

        var userPoints = await _userPointsRepository.SelectByPointSeasonId(pointSeason.Id);

        if (userPoints == null)
        {
            return ResultHelper.Failure<IEnumerable<RankingResponse>>(RankingErrors.PointNotFound);
        }

        var ranking = userPoints.GroupBy(up => new { up.User?.Id, up.User?.Name })
                                .Select(g => new RankingResponse
                                {
                                    Id = g.Key.Id.GetValueOrDefault(),
                                    Name = g.Key.Name,
                                    Points = g.Sum(up => up.Points)
                                }).OrderByDescending(x => x.Points);

        return ResultHelper.Success<IEnumerable<RankingResponse>>(ranking);
    }
}