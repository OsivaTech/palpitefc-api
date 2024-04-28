using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Configs
{
    #region Public Methods

    public static void MapConfigEndpoints(this WebApplication app)
    {
        app.MapGet("/configs", GetConfig)
           .WithSummary("Get a configuration by its name.")
           .WithOpenApi();

        app.MapPost("/configs", CreateOrUpdateConfig)
           .RequireAuthorization("admin")
           .WithSummary("Create or update a configuration.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetConfig(string name, IConfigService service)
    {
        var result = await service.GetAsync(name);

        return result.ToIResult();
    }

    private async static Task<IResult> CreateOrUpdateConfig(ConfigRequest request, IConfigService service)
    {
        var result = await service.CreateOrUpdateAsync(request);

        return result.ToIResult();
    }

    #endregion
}