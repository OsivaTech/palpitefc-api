using Mapster;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Utils;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Errors;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;
using System.Security.Cryptography;

namespace PalpiteApi.Application.Services;

public class AuthService : IAuthService
{
    #region Fields

    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    #endregion

    #region Constructor

    public AuthService(IUserRepository usersRepository, ITokenService tokenService)
    {
        _userRepository = usersRepository;
        _tokenService = tokenService;
    }

    #endregion

    #region Public Methods

    public async Task<Result<AuthResponse>> SignUp(SignUpRequest request, CancellationToken cancellationToken)
    {
        // instanciar a classe Hash
        var hash = new Hash(SHA512.Create());

        // verificar se o email já está cadastrado
        if (await _userRepository.Exists(request.Email!) > 0)
        {
            return ResultHelper.Failure<AuthResponse>(SignUpErrors.EmailAlreadyUsed);
        }

        var guid = Guid.NewGuid();

        var user = new Users()
        {
            Name = request.Name,
            Email = request.Email,
            Password = hash.EncryptPassword(request.Password + guid.ToString()),
            UserGuid = guid.ToString(),
            Role = request.Role!.Value
        };

        // inserir o usuário no banco de dados
        await _userRepository.Insert(user);

        var token = _tokenService.Generate(user);

        var authResponse = new AuthResponse()
        {
            AccessToken = token,
            User = user.Adapt<UserResponse>()
        };

        return ResultHelper.Success(authResponse);
    }

    public async Task<Result<AuthResponse>> SignIn(SignInRequest request, CancellationToken cancellationToken)
    {
        // validar se o email e senha estão corretos
        var users = await _userRepository.FindByEmail(request.Email!);

        if (users.Any() is false)
        {
            //verifica se o usuário existe
            return ResultHelper.Failure<AuthResponse>(SignInErrors.UserNotFound);
        }

        //verifica se a senha está correta
        var hash = new Hash(SHA512.Create());

        if (hash.EncryptPassword(request.Password + users.First().UserGuid) != users.First().Password)
        {
            return ResultHelper.Failure<AuthResponse>(SignInErrors.IncorretPassword);
        }

        var token = _tokenService.Generate(users.First());

        var authResponse = new AuthResponse()
        {
            AccessToken = token,
            User = users.First().Adapt<UserResponse>()
        };

        return ResultHelper.Success(authResponse);
    }

    #endregion
}