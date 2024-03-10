using PalpiteApi.Api.Endpoints;
using PalpiteApi.Api.Endpoints.Authenticated;
using PalpiteApi.Infra.Persistence.Connection;

namespace PalpiteApi.Api.Extensions;

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
        app.MapAuthPalpitationEndpoints();
        app.MapAuthOptionsEndpoints();
        app.MapAuthUserEndpoints();
        app.MapAuthConfigEndpoints();
        app.MapAuthChampionshipEndpoints();
        app.MapAuthNewsEndpoint();
    }

    public static async void InitiaizeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Init();
    }
}
