using PalpiteFC.Api.Endpoints;
using PalpiteFC.Api.Endpoints.Authenticated;
using PalpiteFC.Libraries.Persistence.Database.Connection;

namespace PalpiteFC.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPollEndpoints();
        app.MapLeagueEndpoints();
        app.MapFixtureEndpoints();
        app.MapNewsEndpoints();
        app.MapAuthEndpoints();
        app.MapRankingEndpoints();
        app.MapUrlVideoEndpoints();
        app.MapUserEndpoints();
        app.MapStandingEndpoints();
        app.MapSendEmailEndpoints();

        app.MapAuthGuessEndpoints();
        app.MapAuthOptionsEndpoints();
        app.MapAuthUserEndpoints();
        app.MapAuthConfigEndpoints();
        app.MapAuthLeagueEndpoints();
        app.MapAuthNewsEndpoint();
        app.MapAuthPollEndpoints();
        app.MapAuthTeamEndpoints();
        app.MapAuthFixtureEndpoints();
        app.MapAuthStandingEntpoints();
        app.MapAuthPointSeasonsEndpoints();
    }

    public static async Task InitiaizeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        await context.Init();
    }
}
