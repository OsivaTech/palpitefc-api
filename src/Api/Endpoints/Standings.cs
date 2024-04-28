using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Standings
{
    #region Public Methods

    public static void MapStandingEndpoints(this WebApplication app)
    {
        app.MapGet("/standings", GetAsync)
           .WithSummary("Get all standings.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(IStandingService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    #endregion
}
