using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class User
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/user", async (IUserService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAllAsync(cancellationToken);

            return Results.Ok(result.Value);

        }).RequireAuthorization();
    }
}
