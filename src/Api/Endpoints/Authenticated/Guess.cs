using FluentValidation;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;
using System.Diagnostics;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class Guess
{
    public static void MapAuthGuessEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/palpitation", async (GuessRequest request,
                                                IValidator<GuessRequest> validator,
                                                IGuessService service,
                                                CancellationToken cancellationToken) =>
        {
            Stopwatch watch = new();
            watch.Start();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { message = validationResult.Errors.First().ErrorMessage });
            }

            var result = await service.Create(request, cancellationToken);

            watch.Stop();
            return result.ToIResult(StatusCodes.Status202Accepted);
        }).RequireAuthorization();
    }
}
