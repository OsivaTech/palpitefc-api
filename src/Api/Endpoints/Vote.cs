using MediatR;
using PalpiteApi.Application.Queries;

namespace PalpiteApi.Api.Endpoints;

public static class Vote
{
    public static void MapVoteEndpoints(this WebApplication app)
    {
        app.MapGet("/vote", (IMediator mediator) => mediator.Send(new GetVotesQuery()));
    }
}
