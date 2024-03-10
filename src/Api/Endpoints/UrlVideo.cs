using PalpiteApi.Application.Services.Auth;

namespace PalpiteApi.Api.Endpoints;

public static class UrlVideo
{
    public static void MapUrlVideoEndpoints(this WebApplication app)
    {
        app.MapGet("/urlvideo", async (IConfigService service) => await service.GetAsync("URLvideo"));
    }
}
