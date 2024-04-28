using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Configs
{
    public static void MapConfigEndpoints(this WebApplication app)
    {
        app.MapGet("/configs", async (string name, IConfigService service) =>
        {
            var result = await service.GetAsync(name);

            return result.ToIResult();
        });

        app.MapPost("/configs", async (ConfigRequest request, IConfigService service) =>
        {
            var result = await service.CreateOrUpdateAsync(request);

            return result.ToIResult();
        }).RequireAuthorization("admin");
    }
}
