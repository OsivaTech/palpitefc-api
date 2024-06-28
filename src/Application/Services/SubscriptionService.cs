using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Integrations.Interfaces;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IPagBankProvider _pagBankProvider;
    private readonly IUsersRepository _usersRepository;
    private readonly UserContext _userContext;

    public SubscriptionService(IPagBankProvider pagBankProvider, IUsersRepository usersRepository, UserContext userContext)
    {
        _pagBankProvider = pagBankProvider;
        _usersRepository = usersRepository;
        _userContext = userContext;
    }

    public async Task<Result> CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.Select(_userContext.Id);

        var customer = new Integrations.PagBank.Requests.CreateCustomerRequest()
        {
            Name = user.Name,
            Email = user.Email,
            TaxId = user.Document,
            Phones =
            [
                new()
                {
                    Country = "55",
                    Area = user.PhoneNumber?[..2],
                    Number = user.PhoneNumber?[1..]
                }
            ],
            BillingInfo =
            [
                new()
                {
                    CardInfo = new() { Encrypted = request.Card?.Encrypted, SecurityCode = request.Card?.SecurityCode }
                },
            ]
        };

        var response = await _pagBankProvider.CreateCustomer(customer, cancellationToken);

        if (response.IsFailure)
        {
            return ResultHelper.Failure(SubscriptionErrors.GenericError);
        }

        return ResultHelper.Success();
    }

    public async Task<Result> SubscribeAsync(SubscriptionRequest request, CancellationToken cancellationToken)
    {
        if (request.CreateCustomer)
        {
            await CreateCustomerAsync(new CreateCustomerRequest { Card = request.Card }, cancellationToken);
        }

        var user = await _usersRepository.Select(_userContext.Id);

        var subscription = new Integrations.PagBank.Requests.SubscriptionRequest()
        {
            Plan = new() { Id = "PLAN_883143C0-3B19-446D-B5E1-E3F360E78A9E" },
            Customer = new() { Id = "CUST_863743A5-5320-473C-A494-8B053626D0B" }, //todo change this id to customer id from pagbank
            ReferenceId = user.Id.ToString(),
            PaymentMethods =
            [
                new()
                {
                    Type = "CREDIT_CARD",
                    CardInfo = new() { SecurityCode = request.SecurityCode }
                }
            ]
        };

        var response = await _pagBankProvider.CreateSubscription(subscription, cancellationToken);

        if (response.IsFailure)
        {
            return ResultHelper.Failure(SubscriptionErrors.GenericError);
        }

        await _usersRepository.Update(user);// todo changes this to save (on subscription table?)

        return ResultHelper.Success();
    }
}