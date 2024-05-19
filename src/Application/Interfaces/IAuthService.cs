using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IAuthService
{
    Task<Result<UserResponse>> SignUp(SignUpRequest request, CancellationToken cancellationToken);
    Task<Result<AuthResponse>> SignIn(SignInRequest request, CancellationToken cancellationToken);
    Task<Result<UserResponse>> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken);
}
