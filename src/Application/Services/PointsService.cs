using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class PointsService : IPointsService
{
    private readonly IUserPointsRepository _repository;
    private readonly IGuessesRepository _guessesRepository;
    private readonly UserContext _userContext;

    public PointsService(IUserPointsRepository repository, IGuessesRepository guessesRepository, UserContext userContext)
    {
        _repository = repository;
        _guessesRepository = guessesRepository;
        _userContext = userContext;
    }


    public async Task<Result<IEnumerable<PointsResponse>>> GetCurrentAsync(CancellationToken cancellationToken)
    {
        var startDate = DateTime.MinValue;
        var endDate = DateTime.MaxValue;

        var points = await _repository.SelectByUserId(_userContext.Id, startDate, endDate);
        var guesses = await _guessesRepository.SelectByUserId(_userContext.Id, startDate, endDate);

        var result = new List<PointsResponse>();

        foreach (var item in points)
        {
            result.Add(new PointsResponse
            {
                Fixture = item.Fixture.Adapt<FixtureResponse>(),
                Guess = guesses.FirstOrDefault(w => w.FixtureId == item.FixtureId).Adapt<GuessResponse>(),
                Date = item.CreatedAt,
                Points = item.Points
            });
        }

        return ResultHelper.Success<IEnumerable<PointsResponse>>(result);
    }
}