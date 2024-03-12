using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class Championship
{
    public static void MapChampionshipEndpoints(this WebApplication app)
    {
        app.MapGet("/championship", async (IChampionshipsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });
    }
}
