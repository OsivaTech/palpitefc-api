using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests.Auth;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class Config
{
    public static void MapAuthConfigEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/config", async (string name, IConfigService service) =>
        {
            var result = await service.GetAsync(name);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/config", async (ConfigRequest request, IConfigService service) =>
        {
            var result = await service.CreateOrUpdateAsync(request);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
