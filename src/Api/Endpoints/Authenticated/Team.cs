using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class Team
{
    public static void MapAuthTeamEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/team", async (ITeamService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });
    }
}
