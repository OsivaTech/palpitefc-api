using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Rankings
{
    #region Public Methods

    public static void MapRankingEndpoints(this WebApplication app)
    {
        app.MapGet("/rankings", GetAsync)
           .Produces<IEnumerable<RankingResponse>>()
           .WithSummary("Get all rankings.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(IRankingService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    #endregion
}
