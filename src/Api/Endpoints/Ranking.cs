using PalpiteApi.Application.Responses;

namespace PalpiteApi.Api.Endpoints;

public static class Ranking
{
    public static void MapRankingEndpoints(this WebApplication app)
    {
        app.MapGet("/ranking", () =>
        {
            return new RankingResponse()
            {

            };

        });
    }
}
