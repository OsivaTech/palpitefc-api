using FluentValidation;
using PalpiteApi.Api.Extensions;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;

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

            return result.ToIResult(StatusCodes.Status201Created);
        }).RequireAuthorization();
    }
}
