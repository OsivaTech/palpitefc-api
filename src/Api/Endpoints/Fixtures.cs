using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Fixtures
{
    #region Public Methods

    public static void MapFixtureEndpoints(this WebApplication app)
    {
        app.MapGet("/fixtures", GetFixtures)
           .WithSummary("Get all fixtures.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetFixtures(IFixtureService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    #endregion
}