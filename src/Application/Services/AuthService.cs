using Mapster;
using Microsoft.Extensions.Caching.Memory;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Errors;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;

public class AuthService : IAuthService
{
    #region Fields

    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;
    private readonly IMemoryCache _cache;

    #endregion

    #region Constructor

    public AuthService(IUserRepository usersRepository, ITokenService tokenService, IHashService hashService, IMemoryCache cache)
    {
        _userRepository = usersRepository;
        _tokenService = tokenService;
        _hashService = hashService;
        _cache = cache;
    }

    #endregion

    #region Public Methods

    public async Task<Result<AuthResponse>> SignUp(SignUpRequest request, CancellationToken cancellationToken)
    {
        if (await _userRepository.SelectByEmail(request.Email!) is not null)
        {
            return ResultHelper.Failure<AuthResponse>(SignUpErrors.EmailAlreadyUsed);
        }

        var guid = Guid.NewGuid();

        var user = new Users()
        {
            Name = request.Name,
            Email = request.Email,
            Password = _hashService.EncryptPassword(request.Password + guid.ToString()),
            UserGuid = guid.ToString(),
            Role = request.Role
        };

        await _userRepository.Insert(user);

        var token = _tokenService.Generate(user);

        var authResponse = new AuthResponse()
        {
            AccessToken = token.Data,
            User = user.Adapt<UserResponse>()
        };

        return ResultHelper.Success(authResponse);
    }

    public async Task<Result<AuthResponse>> SignIn(SignInRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectByEmail(request.Email!);

        if (user is null)
        {
            return ResultHelper.Failure<AuthResponse>(SignInErrors.UserNotFound);
        }

        if (_hashService.EncryptPassword(request.Password + user.UserGuid) != user.Password)
        {
            return ResultHelper.Failure<AuthResponse>(SignInErrors.IncorretPassword);
        }

        var token = _tokenService.Generate(user);

        var authResponse = new AuthResponse()
        {
            AccessToken = token.Data,
            User = user.Adapt<UserResponse>()
        };

        return ResultHelper.Success(authResponse);
    }

    public async Task<Result<UserResponse>> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var cacheEntry = _cache.Get<string>($"PasswordReset:{request.Email}");

        if (cacheEntry != request.Code)
        {
            return ResultHelper.Failure<UserResponse>(ResetPasswordErrors.InvalidCode);
        }

        var user = await _userRepository.SelectByEmail(request.Email!);

        var newPassword = _hashService.EncryptPassword(request.Password + user.UserGuid);

        await _userRepository.UpdatePassword(request.Email!, newPassword);

        return ResultHelper.Success(user.Adapt<UserResponse>());
    }

    #endregion
}