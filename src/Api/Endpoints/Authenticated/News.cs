using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests.Auth;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class News
{
    public static void MapAuthNewsEndpoint(this WebApplication app)
    {
        app.MapGet("/auth/news", async (INewsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/news", async (NewsRequest request, INewsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateOrUpdateAsync(request);
            
            return result.ToIResult();
        }).RequireAuthorization();

        app.MapDelete("/auth/news", async (int id, INewsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
