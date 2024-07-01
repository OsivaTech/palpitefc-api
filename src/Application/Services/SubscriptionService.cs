using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Integrations.Interfaces;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    #region Fields

    private readonly IPagBankProvider _pagBankProvider;
    private readonly IUsersRepository _usersRepository;
    private readonly IPlansRepository _plansRepository;
    private readonly ISubscriptionsRepository _subsRepository;
    private readonly UserContext _userContext;

    #endregion

    #region Constructor

    public SubscriptionService(IPagBankProvider pagBankProvider,
                               IUsersRepository usersRepository,
                               IPlansRepository plansRepository,
                               ISubscriptionsRepository subsRepository,
                               UserContext userContext)
    {
        _pagBankProvider = pagBankProvider;
        _usersRepository = usersRepository;
        _plansRepository = plansRepository;
        _subsRepository = subsRepository;
        _userContext = userContext;
    }

    #endregion

    #region Public Methods

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
                    Number = user.PhoneNumber?[2..]
                }
            ],
            BillingInfo =
            [
                new()
                {
                    Type = "CREDIT_CARD",
                    CardInfo = new() { Encrypted = request.Card?.Encrypted, SecurityCode = request.Card?.SecurityCode }
                },
            ]
        };

        var response = await _pagBankProvider.CreateCustomer(customer, cancellationToken);

        if (response.IsFailure)
        {
            return ResultHelper.Failure(SubscriptionErrors.GenericError);
        }

        await _usersRepository.Update(new User()
        {
            Id = user.Id,
            CustomerRef = response.Data.Id
        });

        return ResultHelper.Success();
    }

    public async Task<Result> SubscribeAsync(SubscriptionRequest request, CancellationToken cancellationToken)
    {
        var plans = await _plansRepository.Select();

        if (plans.Any() is false)
        {
            return ResultHelper.Failure(SubscriptionErrors.NoRegistredPlans);
        }

        if (request.CreateCustomer)
        {
            var customerResponse = await CreateCustomerAsync(new CreateCustomerRequest { Card = request.Card }, cancellationToken);

            if (customerResponse.IsFailure)
            {
                return customerResponse;
            }
        }

        var user = await _usersRepository.Select(_userContext.Id);

        var subsRequest = new Integrations.PagBank.Requests.SubscriptionRequest()
        {
            Plan = new() { Id = plans.OrderByDescending(x => x.Id).First().PlanRef },
            Customer = new() { Id = user.CustomerRef },
            ReferenceId = user.Id.ToString(),
            PaymentMethods =
            [
                new()
                {
                    Type = "CREDIT_CARD",
                    CardInfo = new() { SecurityCode = request.Card?.SecurityCode }
                }
            ]
        };

        var response = await _pagBankProvider.CreateSubscription(subsRequest, cancellationToken);

        if (response.IsFailure)
        {
            return ResultHelper.Failure(SubscriptionErrors.GenericError);
        }

        await _subsRepository.Insert(new Subscription
        {
            PlanId = plans.OrderByDescending(x => x.Id).First().Id,
            SubscriptionRef = response.Data.Id,
            UserId = user.Id
        });

        return ResultHelper.Success();
    }

    #endregion
}