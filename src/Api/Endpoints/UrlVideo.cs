using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

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
