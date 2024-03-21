using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class News
{
    public static void MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/news", async (INewsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });
    }
}
