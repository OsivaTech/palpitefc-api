using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class UrlVideo
{
    #region Public Methods

    public static void MapUrlVideoEndpoints(this WebApplication app)
    {
        app.MapGet("/urlvideo", GetAsync)
           .Produces<ConfigResponse>()
           .WithSummary("Get URL video configuration.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(IConfigService service)
    {
        var result = await service.GetAsync("URLvideo");

        return result.ToIResult();
    }

    #endregion
}
