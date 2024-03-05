using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> SignUp(SignUpRequest user, CancellationToken cancellationToken);
    Task<Result<AuthResponse>> SignIn(SignInRequest requestUser, CancellationToken cancellationToken);
}
