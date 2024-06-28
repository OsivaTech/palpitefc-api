using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Integrations.PagBank.Requests;
using PalpiteFC.Api.Integrations.PagBank.Responses;

namespace PalpiteFC.Api.Integrations.Interfaces;

public interface IPagBankProvider
{
    Task<Result<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken);
    Task<Result<SubscriptionResponse>> CreateSubscription(SubscriptionRequest request, CancellationToken cancellationToken);
}