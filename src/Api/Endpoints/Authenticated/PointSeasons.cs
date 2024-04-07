using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class PointSeasons
{
    public static void MapAuthPointSeasonsEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/pointSeasons", async (IPointSeasonsService service,
                                                CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapGet("/auth/pointSeasons/current", async (IPointSeasonsService service,
                                                        CancellationToken cancellationToken) =>
        {
            var result = await service.GetCurrentAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/pointSeasons", async (PointSeasonsRequest request,
                                                IPointSeasonsService service,
                                                CancellationToken cancellationToken) =>
        {
            var result = await service.CreateAsync(request, cancellationToken);

            return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
        }).RequireAuthorization();

        app.MapPut("/auth/pointSeasons", async (PointSeasonsRequest request,
                                                IPointSeasonsService service,
                                                CancellationToken cancellationToken) =>
        {
            var result = await service.UpdateAsync(request, cancellationToken);

            return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
        }).RequireAuthorization();

        app.MapDelete("/auth/pointSeasons", async (int id,
                                                   IPointSeasonsService service,
                                                   CancellationToken cancellationToken) =>
        {
            var result = await service.DeteleAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
