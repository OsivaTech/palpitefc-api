using FluentValidation;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class Auth
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/signup", async (SignUpRequest request,
                                      IValidator<SignUpRequest> validator,
                                      IAuthService service,
                                      CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var result = await service.SignUp(request, cancellationToken);

            return result.ToIResult();
        });

        app.MapPost("/signin", async (SignInRequest request,
                                      IValidator<SignInRequest> validator,
                                      IAuthService service,
                                      CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var result = await service.SignIn(request, cancellationToken);

            return result.ToIResult();
        });

        app.MapPost("/resetpassword", async (ResetPasswordRequest request, IAuthService service, CancellationToken cancellationToken) =>
        {
            var result = await service.ResetPassword(request, cancellationToken);

            return result.ToIResult();
        });
    }
}
