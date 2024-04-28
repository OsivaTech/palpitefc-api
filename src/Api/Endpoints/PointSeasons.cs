 using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class PointSeasons
{
    #region Public Methods

    public static void MapPointSeasonsEndpoints(this WebApplication app)
    {
        app.MapGet("/point-seasons", GetAsync)
           .WithSummary("Get all point seasons.")
           .WithOpenApi();

        app.MapGet("/point-seasons/current", GetCurrentAsync)
           .WithSummary("Get the current point season.")
           .WithOpenApi();

        app.MapPost("/point-seasons", CreateAsync)
           .RequireAuthorization("admin")
           .WithSummary("Create a new point season.")
           .WithOpenApi();

        app.MapPut("/point-seasons", UpdateAsync)
           .RequireAuthorization("admin")
           .WithSummary("Update an existing point season.")
           .WithOpenApi();

        app.MapDelete("/point-seasons", DeleteAsync)
           .RequireAuthorization("admin")
           .WithSummary("Delete a point season.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> GetCurrentAsync(IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.GetCurrentAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> CreateAsync(PointSeasonsRequest request, IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
    }

    private async static Task<IResult> UpdateAsync(PointSeasonsRequest request, IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(request, cancellationToken);

        return result.ToIResult(StatusCodes.Status201Created, StatusCodes.Status409Conflict);
    }

    private async static Task<IResult> DeleteAsync(int id, IPointSeasonsService service, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}