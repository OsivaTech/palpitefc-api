using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Result;
using PalpiteApi.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PalpiteApi.Application.Services;

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

    public Result<string> Generate(Users user)
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

    private static ClaimsIdentity GetClaimsIdentity(Users user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new("id", user.Id.ToString()));
        ci.AddClaim(new("name", user.Name ?? string.Empty));
        ci.AddClaim(new("email", user.Email ?? string.Empty));
        ci.AddClaim(new("role", user.Role.ToString()));
        ci.AddClaim(new("points", user.Points.ToString()));
        ci.AddClaim(new("birthday", user.Birthday ?? string.Empty));
        ci.AddClaim(new("info", user.Info ?? string.Empty));
        ci.AddClaim(new("number", user.Number ?? string.Empty));
        ci.AddClaim(new("team", user.Team ?? string.Empty));

        return ci;
    }

    #endregion
}
