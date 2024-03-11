using PalpiteApi.Application;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class User
{
    public static void MapAuthUserEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/user", async (UserContext userContext)
            => await Task.FromResult(userContext)).RequireAuthorization();

        app.MapPost("/auth/user", async (UserRequest request, IUserService service, CancellationToken cancellationToken) =>
        {
            var result = await service.UpdateAsync(request, cancellationToken);

            return Results.Ok(result.Value);

        }).RequireAuthorization();
    }
}
