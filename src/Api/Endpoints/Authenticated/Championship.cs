using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class Championship
{
    public static void MapAuthChampionshipEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/championship", async (IChampionshipsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/championship", async (ChampionshipRequest request, IChampionshipsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateOrUpdateAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapDelete("/auth/championship", async (int id, IChampionshipsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
