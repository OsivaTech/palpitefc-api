using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Options
{
    public static void MapOptionsEndpoints(this WebApplication app)
    {
        app.MapPost("/options", async (OptionsRequest request,
                                       IOptionsService service,
                                       CancellationToken cancellationToken) =>
        {
            var resultCreate = await service.CreateAsync(request, cancellationToken);

            return resultCreate.ToIResult();
        }).RequireAuthorization();
    }
}