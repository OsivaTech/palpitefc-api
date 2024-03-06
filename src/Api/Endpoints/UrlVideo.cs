namespace PalpiteApi.Api.Endpoints;

public static class UrlVideo
{
    public static void MapUrlVideoEndpoints(this WebApplication app)
    {
        app.MapGet("/urlvideo", () =>
        {
            return "{\"id\":1,\"name\":\"URLvideo\",\"value\":\"https://www.youtube.com/watch?v=7JJ0NreHY-o\"}";
        });
    }

}
