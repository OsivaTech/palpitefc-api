using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
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

        app.MapPost("/news", async (NewsRequest request, INewsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateOrUpdateAsync(request);

            return result.ToIResult();
        }).RequireAuthorization("admin", "journalist");

        app.MapDelete("/news", async (int id, INewsService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization("admin", "journalist");
    }
}
