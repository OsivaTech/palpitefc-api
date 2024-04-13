using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class League
{
    public static void MapAuthLeagueEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/championship", async (ILeagueService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/championship", async (LeagueRequest request, ILeagueService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateOrUpdateAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapDelete("/auth/championship", async (int id, ILeagueService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
