using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Services.Auth;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class Config
{
    public static void MapAuthConfigEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/config", async (string name, IConfigService service) 
            => await service.GetAsync(name)).RequireAuthorization();

        app.MapPost("/auth/config", async (ConfigRequest request, IConfigService service) 
            => await service.CreateOrUpdateAsync(request)).RequireAuthorization();
    }
}
