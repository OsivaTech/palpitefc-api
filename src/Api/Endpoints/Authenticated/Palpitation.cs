using FluentValidation;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

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
