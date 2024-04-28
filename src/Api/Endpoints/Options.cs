using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Options
{
    #region Public Methods

    public static void MapOptionsEndpoints(this WebApplication app)
    {
        app.MapPost("/options", CreateAsync)
           .RequireAuthorization()
           .WithSummary("Create new options.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> CreateAsync(OptionsRequest request, IOptionsService service, CancellationToken cancellationToken)
    {
        var resultCreate = await service.CreateAsync(request, cancellationToken);

        return resultCreate.ToIResult();
    }

    #endregion
}