using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class Options
{
    public static void MapAuthOptionsEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/option", async (OptionsRequest request,
                                           IOptionsService service,
                                           CancellationToken cancellationToken) =>
        {
            if (request.VoteId > 0)
            {
                var resultCreate = await service.CreateAsync(request, cancellationToken);

                return resultCreate.ToIResult();
            }

            var result = await service.ComputeVoteAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}