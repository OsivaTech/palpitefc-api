using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Services.Interfaces;
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

    public string Generate(SignInRequest signInInfo)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_settings.Value.SecurityKey!);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaimsIdentity(signInInfo),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.Add(_settings.Value.Expiration),
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    #endregion

    #region Non-public Methods

    private static ClaimsIdentity GetClaimsIdentity(SignInRequest signInInfo)
    {
        var ci = new ClaimsIdentity();

        return ci;
    }

    #endregion
}
