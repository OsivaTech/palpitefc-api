using Application.Responses;

namespace PalpiteApi.Api.Endpoints;


public static class Game
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        app.MapGet("/game", () =>
        {
            return new GameResponse()
            {
                Id = 1,
                Name = "Jogo 1",
                Start = DateTime.Now,
                ChampionshipId = 1,
                FirstTeam = new TeamGameResponse()
                {
                    Gol = 1,
                    Id = 10,
                    Name = "Atletico",
                    Image = ""
                },
                SecondTeam = new TeamGameResponse()
                {
                    Gol = 2,
                    Id = 20,
                    Name = "Cruzeiro",
                    Image = ""
                }
            };
        });
    }
}
