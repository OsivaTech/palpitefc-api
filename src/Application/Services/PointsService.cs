using PalpiteFC.Api.Application.Enums;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class PointsService : IPointsService
{
    private readonly IUserPointsRepository _repository;
    private readonly UserContext _userContext;

    public PointsService(IUserPointsRepository repository, UserContext userContext)
    {
        _repository = repository;
        _userContext = userContext;
    }

    public async Task<Result<IEnumerable<PointsResponse>>> GetCurrentAsync(CancellationToken cancellationToken)
    {
        var startDate = DateTime.MinValue;
        var endDate = DateTime.MaxValue;

        var points = await _repository.SelectByUserId(_userContext.Id, startDate, endDate);

        var result = points.GroupBy(x => x.FixtureId)
                           .Select(x => new PointsResponse
                           {
                               FixtureId = x.First().FixtureId,
                               GuessId = x.First().GuessId,
                               Date = x.First().Fixture!.Start,
                               Points = x.Select(y => new Points
                               {
                                   Type = (PointType)Enum.Parse(typeof(PointType), y.Type!, true),
                                   Value = y.Points
                               })
                           });

        return ResultHelper.Success<IEnumerable<PointsResponse>>(result);
    }
}