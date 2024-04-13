using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class Poll
{
    public static void MapAuthPollEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/vote", async (IPollService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/vote", async (PollRequest request, IPollService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapDelete("/auth/vote", async (int id, IPollService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
