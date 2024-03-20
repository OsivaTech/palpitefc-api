using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> SignUp(SignUpRequest request, CancellationToken cancellationToken);
    Task<Result<AuthResponse>> SignIn(SignInRequest request, CancellationToken cancellationToken);
    Task<Result<UserResponse>> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken);
}
