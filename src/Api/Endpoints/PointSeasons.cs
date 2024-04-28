 using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class PointSeasons
{
    #region Public Methods

    public static void MapPointSeasonsEndpoints(this WebApplication app)
    {
        app.MapGet("/point-seasons", GetPointSeasons)
           .WithSummary("Get all point seasons.")
           .WithOpenApi();

        app.MapGet("/point-seasons/current", GetCurrentPointSeason)
           .WithSummary("Get the current point season.")
           .WithOpenApi();

        app.MapPost("/point-seasons", CreatePointSeason)
           .RequireAuthorization("admin")
           .WithSummary("Create a new point season.")
           .WithOpenApi();

        app.MapPut("/point-seasons", UpdatePointSeason)
           .RequireAuthorization("admin")
           .WithSummary("Update an existing point season.")
           .WithOpenApi();

        app.MapDelete("/point-seasons", DeletePointSeason)
           .RequireAuthorization("admin")
           .WithSummary("Delete a point season.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetPointSeasons(IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> GetCurrentPointSeason(IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.GetCurrentAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> CreatePointSeason(PointSeasonsRequest request, IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
    }

    private async static Task<IResult> UpdatePointSeason(PointSeasonsRequest request, IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(request, cancellationToken);

        return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
    }

    private async static Task<IResult> DeletePointSeason(int id, IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}