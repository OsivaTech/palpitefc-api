using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class Vote
{
    public static void MapAuthVoteEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/vote", async (IVotesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/vote", async (VoteRequest request, IVotesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapDelete("/auth/vote", async (int id, IVotesService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
