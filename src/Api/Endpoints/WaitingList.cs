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
           .WithSummary("Get all from the waiting list.")
           .WithOpenApi();

        app.MapPost("/waitinglist", InsertAsync)
           .Produces<WaitingListResponse>()
           .AddEndpointFilter<ValidationFilter<WaitingListRequest>>()
           .WithSummary("Update a user.")
           .WithOpenApi();
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
