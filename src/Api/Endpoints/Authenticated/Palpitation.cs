using FluentValidation;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Services.Auth;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class Palpitation
{
    public static void MapAuthPalpitationEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/palpitation", async (PalpitationRequest request,
                                                IValidator<PalpitationRequest> validator,
                                                IPalpitationService service,
                                                CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { message = validationResult.Errors.First().ErrorMessage });
            }

            var result = await service.Create(request, cancellationToken);

            if (result.IsFailure)
            {
                return Results.Conflict(new { message = result.Error });
            }

            return Results.Created();

        }).RequireAuthorization();
    }
}
