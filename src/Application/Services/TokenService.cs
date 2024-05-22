using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PalpiteFC.Api.Application.Enums;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.CrossCutting.Settings;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PalpiteFC.Api.Application.Services;

public class TokenService : ITokenService
{
    #region Fields

    private readonly IOptions<JwtSettings> _settings;

    #endregion

    #region Constructors

    public TokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings;
    }

    #endregion

    #region Public Methods

    public Result<string> Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_settings.Value.SecurityKey!);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaimsIdentity(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.Add(_settings.Value.Expiration),
        };

        var token = handler.CreateToken(tokenDescriptor);

        return ResultHelper.Success(handler.WriteToken(token));
    }

    #endregion

    #region Non-public Methods

    private static ClaimsIdentity GetClaimsIdentity(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new("id", user.Id.ToString()));
        ci.AddClaim(new("name", user.Name ?? string.Empty));
        ci.AddClaim(new("email", user.Email ?? string.Empty));
        ci.AddClaim(new("role", user.Role ?? Roles.User));

        return ci;
    }

    #endregion
}
