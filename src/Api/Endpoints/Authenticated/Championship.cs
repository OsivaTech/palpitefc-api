using PalpiteApi.Application.Services;
using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class Championship
{
    public static void MapAuthChampionshipEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/championship", async (IChampionshipsService service, CancellationToken cancellationToken) 
            => await service.GetAsync(cancellationToken));

        app.MapPost("/auth/championship", async (ChampionshipRequest request, IChampionshipsService service, CancellationToken cancellationToken) 
            => await service.CreateOrUpdateAsync(request, cancellationToken));

        app.MapDelete("/auth/championship", async (int id, IChampionshipsService service, CancellationToken cancellationToken)
            => await service.DeleteAsync(id));
    }
}
