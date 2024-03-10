using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Services.Auth;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class News
{
    public static void MapAuthNewsEndpoint(this WebApplication app)
    {
        app.MapGet("/auth/news", async (INewsService service, CancellationToken cancellationToken)
            => await service.GetAsync(cancellationToken)).RequireAuthorization();

        app.MapPost("/auth/news", async (NewsRequest request, INewsService service, CancellationToken cancellationToken) 
            => await service.CreateOrUpdateAsync(request)).RequireAuthorization();

        app.MapDelete("/auth/news", async (int id, INewsService service, CancellationToken cancellationToken)
            => await service.DeleteAsync(id, cancellationToken)).RequireAuthorization();
    }
}
