using PalpiteApi.Application.Responses;

namespace PalpiteApi.Api.Endpoints;

public static class News
{
    public static void MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/news", () =>
        {
            return new List<NewsResponse>()
            {
                new()
                {
                    Id = 1,
                    Title = "Xibila doidao",
                    Content = "Xibila é pego com dois burgao",
                    UserId = 1,
                    author = new NewsResponse.Author
                    {
                        Id = 1,
                        Name = "Redação do xibila",
                        Team = "Cruzeiro"
                    }
                    
                },
                new()
                {
                    Id = 2,
                    Title = "Noticia do chimbó",
                    Content = "Chimbó é visto usando java",
                    UserId = 2,
                    author = new NewsResponse.Author
                    {
                        Id = 2,
                        Name = "Conteudo do bacon",
                        Team = "Chelsea"
                    }

                }

            };

        });
    }
}
