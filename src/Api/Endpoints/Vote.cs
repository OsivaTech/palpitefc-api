using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class Vote
{
    public static void MapVoteEndpoints(this WebApplication app)
    {
        app.MapGet("/vote", async (IVotesService service, CancellationToken cancellationToken) => await service.GetAsync(cancellationToken));
    }
}