using FluentValidation;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Services.Interfaces;

namespace PalpiteApi.Api.Endpoints;

public static class Auth
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/signup", async (SignUpRequest request,
                                      IValidator<SignUpRequest> validator,
                                      IAuthService service,
                                      CancellationToken cancellationToken) =>
        {
            //valida request
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var result = await service.SignUp(request, cancellationToken);

            if (result.IsFailure)
            {
                Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        });

        app.MapPost("/signin", async (SignInRequest request,
                                      IValidator<SignInRequest> validator,
                                      IAuthService service,
                                      CancellationToken cancellationToken) =>
        {
            //valida request
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var result = await service.SignIn(request, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new { message = result.Error.Description });
            }

            return Results.Ok(result.Value);
        });
    }
}
