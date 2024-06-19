using Mapster;
using MassTransit;
using Microsoft.Extensions.Options;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.CrossCutting.Settings;
using PalpiteFC.Libraries.DataContracts.MessageTypes;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;
public class WaitingListService : IWaitingListService
{
    private readonly IWaitingListRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IOptions<WaitingListSettings> _options;

    public WaitingListService(IWaitingListRepository waitingListRepository, IPublishEndpoint publishEndpoint, IOptions<WaitingListSettings> options)
    {
        _repository = waitingListRepository;
        _publishEndpoint = publishEndpoint;
        _options = options;
    }

    public async Task<Result<IEnumerable<WaitingListResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.Select();

        return ResultHelper.Success(result.Adapt<IEnumerable<WaitingListResponse>>());
    }

    public async Task<Result> InsertAsync(WaitingListRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.CheckIfEmailExists(request.Email!) is true)
        {
            return ResultHelper.Failure<WaitingListResponse>(SignUpErrors.EmailAlreadyUsed);
        }

        await _repository.Insert(request.Adapt<WaitingList>());

        if (_options.Value.SendWelcomeMail)
        {
            await _publishEndpoint.Publish(new WaitingListEmailMessage { ClientName = request.Name, Email = request.Email }, cancellationToken);
        }

        return ResultHelper.Success();
    }

    public async Task<Result> SendWelcomeAsync(IEnumerable<WelcomeRequest> request, CancellationToken cancellationToken)
    {
        foreach (var item in request)
        {
            await _publishEndpoint.Publish(new WaitingListEmailMessage { ClientName = item.Name, Email = item.Email }, cancellationToken);
        }

        return ResultHelper.Success();
    }
}