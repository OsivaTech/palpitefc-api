using FluentValidation;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Guesses
{
    public static void MapGuessEndpoints(this WebApplication app)
    {
        app.MapPost("/guesses", async (GuessRequest request,
                                       IValidator<GuessRequest> validator,
                                       IGuessService service,
                                       CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return validationResult.ToIResult();
            }

            var result = await service.Create(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
