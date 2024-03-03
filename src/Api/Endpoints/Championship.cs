using Application.Responses;

namespace PalpiteApi.Api.Endpoints;

public static class Championship
{
    public static void MapChampionshipEndpoints(this WebApplication app)
    {
        app.MapGet("/championship", () =>
        {
            return new ChampionshipResponse()
            {
                Id = 1,
                Name = "Liga do Pilinha",
                Games = new List<GameResponse>()
               {
                   new GameResponse()
                   {
                       Id = 1,
                       Name = "Jogo 1",
                       Start = DateTime.Now,
                       Finished = true,
                       ChampionshipId = 1
                   },
                   new GameResponse()
                   {
                       Id = 2,
                       Name = "Jogo 2",
                       Start = DateTime.Now,
                       Finished = true,
                       ChampionshipId = 1
                   },
                   new GameResponse()
                   {
                       Id = 3,
                       Name = "Jogo 3",
                       Start = DateTime.Now,
                       Finished = true,
                       ChampionshipId = 1
                   },
                   new GameResponse()
                   {
                       Id = 4,
                       Name = "Jogo 4",
                       Start = DateTime.Now,
                       Finished = true,
                       ChampionshipId = 1
                   },
               }

            };

        });
    }

}
