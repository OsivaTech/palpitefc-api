using PalpiteFC.Api.Application.Enums;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;
using PalpiteFC.Api.Filters;

namespace PalpiteFC.Api.Endpoints;

public static class WaitingList
{
    public static void MapWaitingListEndpoints(this WebApplication app)
    {
        app.MapGet("/waitinglist", GetAllAsync)
           .Produces<IEnumerable<WaitingListResponse>>()
           .RequireAuthorization(Policies.Admin)
           .WithSummary("Get all users from the waiting list.")
           .WithOpenApi();

        app.MapPost("/waitinglist/send-welcome", SendWelcome)
           .Produces(204)
           .RequireAuthorization(Policies.Admin)
           .WithSummary("Send welcome mail to user list.")
           .WithOpenApi();

        app.MapPost("/waitinglist", InsertAsync)
           .Produces<WaitingListResponse>()
           .AddEndpointFilter<ValidationFilter<WaitingListRequest>>()
           .WithSummary("Subscribe to waiting list.")
           .WithOpenApi();
    }

    private async static Task<IResult> SendWelcome(WelcomeRequest[] request, IWaitingListService service, CancellationToken cancellationToken)
    {
        var result = await service.SendWelcomeAsync(request, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> GetAllAsync(IWaitingListService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> InsertAsync(WaitingListRequest request, IWaitingListService service, CancellationToken cancellationToken)
    {
        var result = await service.InsertAsync(request, cancellationToken);

        return result.ToIResult();
    }
}
