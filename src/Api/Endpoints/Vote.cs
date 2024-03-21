using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Vote
{
    public static void MapVoteEndpoints(this WebApplication app)
    {
        app.MapGet("/vote", async (IVotesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });
    }
}