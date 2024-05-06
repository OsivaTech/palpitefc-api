
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Points
{
    public static void MapPointsEndpoints(this WebApplication app)
    {
        app.MapGet("/points/me", GetAsync)
           .Produces<IEnumerable<PointsResponse>>()
           .RequireAuthorization()
           .WithSummary("Get points of current user.")
           .WithOpenApi();
    }

    private static async Task<IResult> GetAsync(IPointsService service, CancellationToken cancellationToken)
    {
        var result = await service.GetCurrentAsync(cancellationToken);

        return result.ToIResult();
    }
}
