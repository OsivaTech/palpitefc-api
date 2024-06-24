using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;
public interface ISubscriptionService
{
    Task<Result<SubscriptionResponse>> SubscribeAsync(SubscriptionRequest request, CancellationToken cancellationToken);
}