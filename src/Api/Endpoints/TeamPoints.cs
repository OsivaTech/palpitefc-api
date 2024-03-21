using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

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
