using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;
using PalpiteFC.Api.Filters;

namespace PalpiteFC.Api.Endpoints;

public static class Guesses
{
    #region Public Methods

    public static void MapGuessEndpoints(this WebApplication app)
    {
        app.MapGet("/guesses/me", GetOwnAsync)
           .Produces<IEnumerable<GuessResponse>>()
           .RequireAuthorization()
           .WithSummary("Get all guesses of the current user.")
           .WithOpenApi();

        app.MapPost("/guesses", CreateAsync)
           .Produces<GuessResponse>()
           .RequireAuthorization()
           .AddEndpointFilter<ValidationFilter<GuessRequest>>()
           .WithSummary("Create a new guess.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetOwnAsync(DateTime? startDate, DateTime? endDate, IGuessService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(startDate, endDate, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> CreateAsync(GuessRequest request, IGuessService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}
