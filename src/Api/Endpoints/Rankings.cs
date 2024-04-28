using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Rankings
{
    #region Public Methods

    public static void MapRankingEndpoints(this WebApplication app)
    {
        app.MapGet("/rankings", GetRankings)
           .WithSummary("Get all rankings.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetRankings(IRankingService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    #endregion
}
