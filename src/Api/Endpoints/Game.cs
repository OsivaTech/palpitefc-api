using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class Game
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        app.MapGet("/game", async (IGamesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });
    }
}
