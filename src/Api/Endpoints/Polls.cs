using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Polls
{
    #region Public Methods

    public static void MapPollEndpoints(this WebApplication app)
    {
        app.MapGet("/polls", GetAsync)
           .Produces<IEnumerable<PollResponse>>()
           .WithSummary("Get all polls.")
           .WithOpenApi();

        app.MapPost("/polls", CreateAsync)
           .Produces<PollResponse>()
           .RequireAuthorization("admin")
           .WithSummary("Create a new poll.")
           .WithOpenApi();

        app.MapDelete("/polls/{id}", DeleteAsync)
           .Produces(StatusCodes.Status204NoContent)
           .RequireAuthorization("admin")
           .WithSummary("Delete a poll.")
           .WithOpenApi();

        app.MapPost("/polls/{id}/vote/{optionId}", ComputeVoteAsync)
           .Produces<PollResponse>()
           .RequireAuthorization()
           .WithSummary("Submit a vote on a poll.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(IPollService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> CreateAsync(PollRequest request, IPollService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> DeleteAsync(int id, IPollService service, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> ComputeVoteAsync(int id, int optionId, IPollService service, CancellationToken cancellationToken)
    {
        var result = await service.ComputeVoteAsync(id, optionId, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}
