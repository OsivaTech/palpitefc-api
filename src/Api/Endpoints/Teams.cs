using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Teams
{
    #region Public Methods

    public static void MapTeamEndpoints(this WebApplication app)
    {
        app.MapGet("/teams", GetAsync)
           .Produces<IEnumerable<TeamResponse>>()
           .WithSummary("Get all teams.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(ITeamService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    #endregion
}

