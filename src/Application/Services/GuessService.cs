using Mapster;
using MassTransit;
using Microsoft.Extensions.Options;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.CrossCutting.Settings;
using PalpiteFC.Libraries.DataContracts.MessageTypes;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;
public class GuessService : IGuessService
{
    private readonly IGuessesRepository _repository;
    private readonly IFixturesRepository _fixturesRepository;
    private readonly ICacheService _cacheService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly UserContext _userContext;
    private readonly IOptions<GuessesSettings> _settings;

    public GuessService(IGuessesRepository repository,
                        IFixturesRepository fixturesRepository,
                        ICacheService cacheService,
                        IPublishEndpoint publishEndpoint,
                        UserContext userContext,
                        IOptions<GuessesSettings> settings)
    {
        _repository = repository;
        _fixturesRepository = fixturesRepository;
        _cacheService = cacheService;
        _publishEndpoint = publishEndpoint;
        _userContext = userContext;
        _settings = settings;
    }

    public async Task<Result<GuessResponse>> CreateAsync(GuessRequest request, CancellationToken cancellationToken)
    {
        var result = new GuessResponse();
        var guessDate = DateTime.UtcNow;

        var cacheKey = $"PalpiteFC:Guesses:{_userContext.Id}_{request.FixtureId}";

        var exitsKey = await _cacheService.ExistsKey(cacheKey, cancellationToken);

        if (exitsKey)
        {
            return ResultHelper.Failure<GuessResponse>(GuessErrors.GuessAlreadyExists);
        }

        var fixture = await _fixturesRepository.Select(request.FixtureId);

        if (fixture is null)
        {
            return ResultHelper.Failure<GuessResponse>(FixtureErrors.FixtureNotFound);
        }

        if (fixture.Start < DateTime.UtcNow)
        {
            return ResultHelper.Failure<GuessResponse>(GuessErrors.MatchAlreadyStarted);
        }

        var guesses = await _repository.SelectByUserIdAndFixtureId(_userContext.Id, request.FixtureId);

        if (guesses.Any())
        {
            return ResultHelper.Failure<GuessResponse>(GuessErrors.GuessAlreadyExists);
        }


        if (_settings.Value.UseQueue)
        {
            var message = request.Adapt<GuessMessage>();
            message.GuessDate = guessDate;
            message.UserId = _userContext.Id;

            await _publishEndpoint.Publish(message, cancellationToken);
        }
        else
        {
            var entity = request.Adapt<Guess>();
            entity.GuessDate = guessDate;
            entity.UserId = _userContext.Id;

            var id = await _repository.InsertAndGetId(entity);

            var guess = await _repository.Select(id);

            result = guess.Adapt<GuessResponse>();
        }


        await _cacheService.CreateAsync(cacheKey,
                                        string.Empty,
                                        absoluteExpiration: DateTime.UtcNow.Date.AddDays(1).AddTicks(-1),
                                        cancellationToken: cancellationToken);

        return ResultHelper.Success(result);
    }

    public async Task<Result<IEnumerable<GuessResponse>>> GetAsync(DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
    {
        if (startDate > endDate)
        {
            return ResultHelper.Failure<IEnumerable<GuessResponse>>(OtherErrors.StartDateLaterThanEnd);
        }

        var guesses = await _repository.SelectByUserId(_userContext.Id, startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);

        return ResultHelper.Success(guesses.Adapt<IEnumerable<GuessResponse>>());
    }
}
