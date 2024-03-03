using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class Game
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        app.MapGet("/game", async (IGamesService service, CancellationToken cancellationToken) => await service.GetAsync(cancellationToken));
    }
}
