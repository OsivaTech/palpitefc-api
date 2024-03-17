using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests.Auth;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class Game
{
    public static void MapAuthGameEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/game", async (IGamesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });

        app.MapPost("/auth/game", async (GameRequest request, IGamesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateOrUpdateAsync(request, cancellationToken);

            return result.ToIResult();
        });

        app.MapDelete("/auth/game", async (int id, IGamesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        });
    }
}
