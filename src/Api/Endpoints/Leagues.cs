using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Leagues
{
    #region Public Methods

    public static void MapLeagueEndpoints(this WebApplication app)
    {
        app.MapGet("/leagues", GetAsync)
           .Produces<IEnumerable<LeagueResponse>>()
           .WithSummary("Get all leagues.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(ILeagueService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    #endregion
}