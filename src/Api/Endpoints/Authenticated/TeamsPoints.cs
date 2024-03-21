using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class TeamsPoints
{
    public static void MapAuthTeamPointsEntpoints(this WebApplication app)
    {
        app.MapGet("/auth/championshipTeamPoints", async (ITeamsPointsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });

        app.MapPost("/auth/championshipTeamPoints", async (ChampionshipTeamsPointsRequest request, ITeamsPointsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateOrUpdateAsync(request, cancellationToken);

            return result.ToIResult();
        });
    }
}
