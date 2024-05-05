using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Configs
{
    #region Public Methods

    public static void MapConfigEndpoints(this WebApplication app)
    {
        app.MapGet("/configs", GetAsync)
           .Produces<ConfigResponse>()
           .WithSummary("Get a configuration by its name.")
           .WithOpenApi();

        app.MapPost("/configs", CreateAsync)
           .Produces<ConfigResponse>()
           .RequireAuthorization("admin")
           .WithSummary("Create a configuration.")
           .WithOpenApi();

        app.MapPut("/configs/{id}", UpdateAsync)
           .Produces<ConfigResponse>()
           .RequireAuthorization("admin")
           .WithSummary("Update a configuration.")
           .WithOpenApi();

        app.MapDelete("/configs/{id}", DeleteAsync)
           .Produces(StatusCodes.Status204NoContent)
           .RequireAuthorization("admin")
           .WithSummary("Delete a configuration.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(string name, IConfigService service)
    {
        var result = await service.GetAsync(name);

        return result.ToIResult();
    }

    private async static Task<IResult> CreateAsync(ConfigRequest request, IConfigService service)
    {
        var result = await service.CreateAsync(request);

        return result.ToIResult();
    }

    private async static Task<IResult> UpdateAsync(int id, ConfigRequest request, IConfigService service)
    {
        var result = await service.UpdateAsync(id, request);

        return result.ToIResult();
    }

    private async static Task<IResult> DeleteAsync(int id, IConfigService service)
    {
        var result = await service.DeleteAsync(id);

        return result.ToIResult();
    }

    #endregion
}