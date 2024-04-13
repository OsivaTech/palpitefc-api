using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class Standing
{
    public static void MapAuthStandingEntpoints(this WebApplication app)
    {
        app.MapGet("/auth/championshipTeamPoints", async (IStandingService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });

        app.MapPost("/auth/championshipTeamPoints", async (StandingRequest request, IStandingService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateOrUpdateAsync(request, cancellationToken);

            return result.ToIResult();
        });
    }
}
