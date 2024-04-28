using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Polls
{
    #region Public Methods

    public static void MapPollEndpoints(this WebApplication app)
    {
        app.MapGet("/polls", GetPolls)
           .WithSummary("Get all polls.")
           .WithOpenApi();

        app.MapPost("/polls/vote", ComputeVote)
           .RequireAuthorization()
           .WithSummary("Submit a vote on a poll.")
           .WithOpenApi();

        app.MapPost("/polls", CreatePoll)
           .RequireAuthorization("admin")
           .WithSummary("Create a new poll.")
           .WithOpenApi();

        app.MapDelete("/polls", DeletePoll)
           .RequireAuthorization("admin")
           .WithSummary("Delete a poll.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetPolls(IPollService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> ComputeVote(OptionsRequest request, IOptionsService service, CancellationToken cancellationToken)
    {
        var result = await service.ComputeVoteAsync(request, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> CreatePoll(PollRequest request, IPollService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> DeletePoll(int id, IPollService service, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}
