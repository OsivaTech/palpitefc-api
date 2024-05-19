using PalpiteFC.Api.Application.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace PalpiteFC.Api.Middlewares;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context, UserContext userContext)
    {
        var header = context.Request.Headers.Authorization;

        if (header.Count == 0)
        {
            await _next(context);
            return;
        }

        var stringToken = (header[0] ?? string.Empty).Replace("Bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();

        var canRead = tokenHandler.CanReadToken(stringToken);

        if (canRead is false)
        {
            await _next(context);
            return;
        }

        var jsonToken = tokenHandler.ReadToken(stringToken) as JwtSecurityToken;

        if (jsonToken is not null)
        {
            userContext.Id = ConvertToInt(jsonToken.Claims.First(w => w.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase)).Value);
            userContext.Name = jsonToken.Claims.First(w => w.Type.Equals("Name", StringComparison.InvariantCultureIgnoreCase)).Value;
            userContext.Email = jsonToken.Claims.First(w => w.Type.Equals("Email", StringComparison.InvariantCultureIgnoreCase)).Value;
            userContext.Role = ConvertToInt(jsonToken.Claims.First(w => w.Type.Equals("Role", StringComparison.InvariantCultureIgnoreCase)).Value);
        }

        await _next(context);

        static int ConvertToInt(string value)
        {
            var number = string.IsNullOrWhiteSpace(value) ? 0 : Convert.ToInt32(value);

            return number;
        }
    }
}
