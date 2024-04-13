using Mapster;
using MassTransit;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.DataContracts.MessageTypes;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;
public class GuessService : IGuessService
{
    private readonly IGuessesRepository _repository;
    private readonly IFixturesRepository _fixturesRepository;
    private readonly ICacheService _cacheService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly UserContext _userContext;

    public GuessService(IGuessesRepository repository,
                        IFixturesRepository fixturesRepository,
                        ICacheService cacheService,
                        IPublishEndpoint publishEndpoint,
                        UserContext userContext)
    {
        _repository = repository;
        _fixturesRepository = fixturesRepository;
        _cacheService = cacheService;
        _publishEndpoint = publishEndpoint;
        _userContext = userContext;
    }

    public async Task<Result<GuessResponse>> Create(GuessRequest request, CancellationToken cancellationToken)
    {
        var cacheKey = $"PalpiteFC:Guesses:{_userContext.Id}_{request.GameId}";

        var exitsKey = await _cacheService.ExistsKey(cacheKey, cancellationToken);

        if (exitsKey)
        {
            return ResultHelper.Failure<GuessResponse>(GuessErrors.GuessAlreadyExists);
        }

        var fixture = await _fixturesRepository.Select(request.GameId);

        if (fixture is null)
        {
            return ResultHelper.Failure<GuessResponse>(FixtureErrors.FixtureNotFound);
        }

        if (fixture.Start < DateTime.Now)
        {
            return ResultHelper.Failure<GuessResponse>(GuessErrors.MatchAlreadyStarted);
        }

        var guesses = await _repository.SelectByUserIdAndGameId(_userContext.Id, request.GameId);

        if (guesses.Any())
        {
            return ResultHelper.Failure<GuessResponse>(GuessErrors.GuessAlreadyExists);
        }

        var message = request.Adapt<GuessMessage>();

        message.UserId = _userContext.Id;

        await _publishEndpoint.Publish(message, cancellationToken);

        await _cacheService.CreateAsync(cacheKey,
                                        message,
                                        absoluteExpiration: DateTime.Now.Date.AddDays(1).AddTicks(-1),
                                        cancellationToken: cancellationToken);

        return ResultHelper.Success(new GuessResponse());
    }
}
