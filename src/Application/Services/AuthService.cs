using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class AuthService : IAuthService
{
    #region Fields

    private readonly IUsersRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;
    private readonly IDistributedCache _cache;

    #endregion

    #region Constructor

    public AuthService(IUsersRepository usersRepository, ITokenService tokenService, IHashService hashService, IDistributedCache cache)
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

        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = _hashService.EncryptPassword(request.Password + guid.ToString()),
            UserGuid = guid.ToString(),
            Role = 300
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
        var cacheEntry = _cache.GetString($"PasswordReset:{request.Email}");

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