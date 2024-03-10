using PalpiteApi.Application;

namespace PalpiteApi.Api.Endpoints.Authenticated;


public static class User
{
    public static void MapAuthUserEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/user", async (UserContext userContext) 
            => await Task.FromResult(userContext)).RequireAuthorization();
    }
}
