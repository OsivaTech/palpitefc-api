using PalpiteApi.Application.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class Ranking
{
    public static void MapRankingEndpoints(this WebApplication app)
    {
        app.MapGet("/ranking", async (IRankingService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);
            return result.Value;
        });
    }
}
