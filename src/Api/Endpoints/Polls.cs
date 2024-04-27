using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Polls
{
    public static void MapPollEndpoints(this WebApplication app)
    {
        app.MapGet("/polls", async (IPollService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAsync(cancellationToken);

            return result.ToIResult();
        });

        app.MapPost("/polls/vote", async (OptionsRequest request,
                                          IOptionsService service,
                                          CancellationToken cancellationToken) =>
        {
            var result = await service.ComputeVoteAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/polls", async (PollRequest request, IPollService service, CancellationToken cancellationToken) =>
        {
            var result = await service.CreateAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization("admin");

        app.MapDelete("/polls", async (int id, IPollService service, CancellationToken cancellationToken) =>
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization("admin");
    }
}