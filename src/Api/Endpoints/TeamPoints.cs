using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class TeamPoints
{
    public static void MapTeamPointsEndpoints(this WebApplication app)
    {
        app.MapGet("/championshipTeamPoints", async (ITeamsPointsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });
    }
}
