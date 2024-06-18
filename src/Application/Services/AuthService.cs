using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using PalpiteFC.Api.Application.Enums;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Connection;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class AuthService : IAuthService
{
    #region Fields

    private readonly IUsersRepository _userRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly IUsersAddressesRepository _userAddressesRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;
    private readonly IDistributedCache _cache;
    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region Constructor

    public AuthService(IUsersRepository usersRepository,
                       ITeamsRepository teamsRepository,
                       IUsersAddressesRepository userAddressesRepository,
                       ITokenService tokenService,
                       IHashService hashService,
                       IDistributedCache cache,
                       IUnitOfWork unitOfWork)
    {
        _userRepository = usersRepository;
        _teamsRepository = teamsRepository;
        _userAddressesRepository = userAddressesRepository;
        _tokenService = tokenService;
        _hashService = hashService;
        _cache = cache;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Public Methods

    public async Task<Result<UserResponse>> SignUp(SignUpRequest request, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();
        int? addressId = null;

        _unitOfWork.BeginTransaction();

        if (await _userRepository.SelectByEmail(request.Email!) is not null)
        {
            return ResultHelper.Failure<UserResponse>(SignUpErrors.EmailAlreadyUsed);
        }

        if (await _teamsRepository.Select(request.TeamId) is null)
        {
            return ResultHelper.Failure<UserResponse>(TeamErrors.InvalidTeamId);
        }

        if (request.Address is not null)
        {
            addressId = await _userAddressesRepository.InsertAndGetId(request.Address.Adapt<UserAddress>());
        }

        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = _hashService.EncryptPassword(request.Password + guid.ToString()),
            Gender = request.Gender.ToString(),
            Document = request.Document,
            TeamId = request.TeamId,
            PhoneNumber = request.PhoneNumber,
            Birthday = request.Birthday,
            AddressId = addressId,
            Role = Roles.User,
            UserGuid = guid.ToString()
        };

        var createdId = await _userRepository.InsertAndGetId(user);

        var createdUser = await _userRepository.Select(createdId);

        _unitOfWork.Commit();

        return ResultHelper.Success(createdUser.Adapt<UserResponse>());
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

        if (user is null)
        {
            return ResultHelper.Failure<UserResponse>(SignInErrors.UserNotFound);
        }

        var newPassword = _hashService.EncryptPassword(request.Password + user.UserGuid);

        await _userRepository.UpdatePassword(request.Email!, newPassword);

        return ResultHelper.Success(user.Adapt<UserResponse>());
    }

    #endregion
}