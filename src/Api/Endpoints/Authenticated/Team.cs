using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;

namespace PalpiteApi.Api.Endpoints.Authenticated;

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
