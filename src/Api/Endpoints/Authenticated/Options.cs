using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Services.Interfaces;

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

                if (resultCreate.IsFailure)
                {
                    return Results.BadRequest(new { message = resultCreate.Error.Description });
                }

                return Results.Ok(resultCreate.Value);
            }

            var result = await service.ComputeVoteAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { message = result.Error.Description });
            }

            return Results.Ok(result.Value);

        }).RequireAuthorization();
    }
}