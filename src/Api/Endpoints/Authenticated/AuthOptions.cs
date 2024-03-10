using FluentValidation;
using Org.BouncyCastle.Asn1.Ocsp;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints.Authenticated;

public static class AuthOptions
{
    public static void MapAuthOptionsEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/option", async (OptionsRequest request, IValidator<OptionsRequest> validator, IOptionsService service, CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(new { message = "Erro de validação" });
            }
            var result = await service.SendOption(request, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { message = result.Error.Description });
            }

            return Results.Ok(result.Value);
        });
    }
}
