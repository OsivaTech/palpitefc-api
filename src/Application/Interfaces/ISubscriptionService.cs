using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;
public interface ISubscriptionService
{
    Task<Result> CreateCustomerAsync(CreateCustomerRequest request, CancellationToken cancellationToken);
    Task<Result> SubscribeAsync(SubscriptionRequest request, CancellationToken cancellationToken);
}