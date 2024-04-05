using PalpiteFC.Api.Endpoints;
using PalpiteFC.Api.Endpoints.Authenticated;
using PalpiteFC.Api.Persistence.Connection;

namespace PalpiteFC.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapVoteEndpoints();
        app.MapChampionshipEndpoints();
        app.MapGameEndpoints();
        app.MapNewsEndpoints();
        app.MapAuthEndpoints();
        app.MapRankingEndpoints();
        app.MapUrlVideoEndpoints();
        app.MapUserEndpoints();
        app.MapTeamPointsEndpoints();
        app.MapSendEmailEndpoints();

        app.MapAuthPalpitationEndpoints();
        app.MapAuthOptionsEndpoints();
        app.MapAuthUserEndpoints();
        app.MapAuthConfigEndpoints();
        app.MapAuthChampionshipEndpoints();
        app.MapAuthNewsEndpoint();
        app.MapAuthVoteEndpoints();
        app.MapAuthTeamEndpoints();
        app.MapAuthGameEndpoints();
        app.MapAuthTeamPointsEntpoints();
    }

    public static async void InitiaizeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Init();
    }
}
