using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;
using PalpiteFC.Api.Filters;

namespace PalpiteFC.Api.Endpoints;

public static class Subscriptions
{
    #region Public Methods

    public static void MapSubscriptionsEndpoints(this WebApplication app)
    {
        app.MapPost("/customer", CreateCustomerAsync)
           .Produces(StatusCodes.Status201Created)
           .RequireAuthorization()
           .WithSummary("Create a customer account.")
           .WithOpenApi();

        app.MapPost("/subscriptions", SubscribeAsync)
           .Produces(StatusCodes.Status204NoContent)
           .RequireAuthorization()
           .AddEndpointFilter<ValidationFilter<SubscriptionRequest>>()
           .WithSummary("Subscribe to premium plan.")
           .WithOpenApi();
    }

    #endregion

    #region Non-public Methods

    private static async Task<IResult> CreateCustomerAsync(CreateCustomerRequest request, ISubscriptionService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateCustomerAsync(request, cancellationToken);

        return result.ToIResult(201);
    }

    private static async Task<IResult> SubscribeAsync(SubscriptionRequest request, ISubscriptionService service, CancellationToken cancellationToken)
    {
        var result = await service.SubscribeAsync(request, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}