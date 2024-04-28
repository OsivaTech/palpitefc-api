using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Teams
{
    #region Public Methods

    public static void MapTeamEndpoints(this WebApplication app)
    {
        app.MapGet("/teams", GetTeams)
           .WithSummary("Get all teams.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetTeams(ITeamService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    #endregion
}

