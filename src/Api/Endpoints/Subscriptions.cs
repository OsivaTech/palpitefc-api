using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Subscriptions
{
    #region Public Methods

    public static void MapSubscriptionsEndpoints(this WebApplication app)
    {
        app.MapPost("/subscriptions", SubscribeAsync)
           .Produces<SubscriptionResponse>()
           .RequireAuthorization()
           .WithSummary("Subscribe to premium plan.")
           .WithOpenApi();
    }

    #endregion

    #region Non-public Methods

    private static async Task<IResult> SubscribeAsync(SubscriptionRequest request, ISubscriptionService service, CancellationToken cancellationToken)
    {
        var result = await service.SubscribeAsync(request, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}
