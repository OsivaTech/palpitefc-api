using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class UrlVideo
{
    public static void MapUrlVideoEndpoints(this WebApplication app)
    {
        app.MapGet("/urlvideo", async (IConfigService service) =>
        {
            var result = await service.GetAsync("URLvideo");

            return result.ToIResult();
        });
    }
}
