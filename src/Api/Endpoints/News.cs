using PalpiteApi.Application.Services.Auth;

namespace PalpiteApi.Api.Endpoints;

public static class News
{
    public static void MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/news", async (INewsService service, CancellationToken cancellationToken) 
            => await service.GetAsync(cancellationToken));
    }
}
