using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Fixtures
{
    public static void MapFixtureEndpoints(this WebApplication app)
    {
        app.MapGet("/fixtures", async (IFixtureService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });
    }
}
