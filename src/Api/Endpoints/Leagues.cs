using PalpiteFC.Api.Application.Enums;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Leagues
{
    #region Public Methods

    public static void MapLeagueEndpoints(this WebApplication app)
    {
        app.MapGet("/leagues", GetEnabledAsync)
           .Produces<IEnumerable<LeagueResponse>>()
           .WithSummary("Get all leagues.")
           .WithOpenApi();

        app.MapPost("/leagues/update", UpdateAsync)
           .Produces(StatusCodes.Status204NoContent)
           .RequireAuthorization(Policies.Admin)
           .WithSummary("Update leagues database")
           .WithOpenApi();

        app.MapPut("/leagues/{id}", ToggleStatusAsync)
           .Produces<IEnumerable<LeagueResponse>>()
           .RequireAuthorization(Policies.Admin)
           .WithSummary("Enable/Disable league showing.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetEnabledAsync(ILeagueService service, CancellationToken cancellationToken)
    {
        var result = await service.GetEnabledAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> UpdateAsync(ILeagueService service, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> ToggleStatusAsync(int id, bool enabled, ILeagueService service, CancellationToken cancellationToken)
    {
        var result = await service.ToggleStatusAsync(id, enabled, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}