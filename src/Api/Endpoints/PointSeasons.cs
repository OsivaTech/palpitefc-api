 using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class PointSeasons
{
    public static void MapPointSeasonsEndpoints(this WebApplication app)
    {
        app.MapGet("/point-seasons", async (IPointSeasonsService service,
                                           CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });

        app.MapGet("/point-seasons/current", async (IPointSeasonsService service,
                                                   CancellationToken cancellationToken) =>
        {
            var result = await service.GetCurrentAsync(cancellationToken);

            return result.ToIResult();
        });

        app.MapPost("/point-seasons", async (PointSeasonsRequest request,
                                            IPointSeasonsService service,
                                            CancellationToken cancellationToken) =>
        {
            var result = await service.CreateAsync(request, cancellationToken);

            return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
        }).RequireAuthorization("admin");

        app.MapPut("/point-seasons", async (PointSeasonsRequest request,
                                           IPointSeasonsService service,
                                           CancellationToken cancellationToken) =>
        {
            var result = await service.UpdateAsync(request, cancellationToken);

            return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
        }).RequireAuthorization("admin");

        app.MapDelete("/point-seasons", async (int id,
                                              IPointSeasonsService service,
                                              CancellationToken cancellationToken) =>
        {
            var result = await service.DeteleAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization("admin");
    }
}
