using PalpiteFC.Api.Endpoints;
using PalpiteFC.Libraries.Persistence.Database.Connection;

namespace PalpiteFC.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapConfigEndpoints();
        app.MapFixtureEndpoints();
        app.MapGuessEndpoints();
        app.MapLeagueEndpoints();
        app.MapNewsEndpoints();
        app.MapPointSeasonsEndpoints();
        app.MapPollEndpoints();
        app.MapRankingEndpoints();
        app.MapSendEmailEndpoints();
        app.MapStandingEndpoints();
        app.MapTeamEndpoints();
        app.MapUrlVideoEndpoints();
        app.MapUserEndpoints();
        app.MapPointsEndpoints(); 
        app.MapAdvertisementEndpoints();
    }

    public static async Task InitiaizeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Init();
    }
}
