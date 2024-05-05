using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;
using PalpiteFC.Api.Filters;

namespace PalpiteFC.Api.Endpoints;

public static class Auth
{
    #region Public Methods

    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/signup", SignUp)
           .Produces<UserResponse>()
           .AddEndpointFilter<ValidationFilter<SignUpRequest>>()
           .WithSummary("Sign up a new user.")
           .WithOpenApi();

        app.MapPost("/signin", SignIn)
           .Produces<UserResponse>()
           .AddEndpointFilter<ValidationFilter<SignInRequest>>()
           .WithSummary("Sign in an existing user.")
           .WithOpenApi();

        app.MapPost("/resetpassword", ResetPassword)
           .WithSummary("Reset the password of a user.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods
    private async static Task<IResult> SignUp(SignUpRequest request, IAuthService service, CancellationToken cancellationToken)
    {
        var result = await service.SignUp(request, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> SignIn(SignInRequest request, IAuthService service, CancellationToken cancellationToken)
    {
        var result = await service.SignIn(request, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> ResetPassword(ResetPasswordRequest request, IAuthService service, CancellationToken cancellationToken)
    {
        var result = await service.ResetPassword(request, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}