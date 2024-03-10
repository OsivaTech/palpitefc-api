using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class AuthVote
{
    public static void MapAuthVoteEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/vote", async (IVotesService service, CancellationToken cancellationToken) => await service.GetAsync(cancellationToken)).RequireAuthorization();
    }
}
