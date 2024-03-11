using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class Vote
{
    public static void MapAuthVoteEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/vote", async (IVotesService service, CancellationToken cancellationToken)
            => await service.GetAsync(cancellationToken)).RequireAuthorization();

        app.MapPost("/auth/vote", async (VoteRequest request, IVotesService service, CancellationToken cancellationToken)
            => await service.CreateAsync(request, cancellationToken)).RequireAuthorization();

        app.MapDelete("/auth/vote", async (int id, IVotesService service, CancellationToken cancellationToken)
            => await service.DeleteAsync(id, cancellationToken)).RequireAuthorization();
    }
}
