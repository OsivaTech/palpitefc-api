using PalpiteApi.Application;
using System.IdentityModel.Tokens.Jwt;

namespace PalpiteApi.Api.Middlewares;

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

        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken((header[0] ?? string.Empty).Replace("Bearer ", "")) as JwtSecurityToken;

        if (jsonToken is not null)
        {
            userContext.Id = Convert.ToInt32(jsonToken.Claims.First(w => w.Type.Equals("Id", StringComparison.InvariantCultureIgnoreCase)).Value);
            userContext.Name = jsonToken.Claims.First(w => w.Type.Equals("Name", StringComparison.InvariantCultureIgnoreCase)).Value;
            userContext.Email = jsonToken.Claims.First(w => w.Type.Equals("Email", StringComparison.InvariantCultureIgnoreCase)).Value;
            userContext.Birthday = jsonToken.Claims.First(w => w.Type.Equals("Birthday", StringComparison.InvariantCultureIgnoreCase)).Value;
            userContext.Role = Convert.ToInt32(jsonToken.Claims.First(w => w.Type.Equals("Role", StringComparison.InvariantCultureIgnoreCase)).Value);
            userContext.Points = Convert.ToInt32(jsonToken.Claims.First(w => w.Type.Equals("Points", StringComparison.InvariantCultureIgnoreCase)).Value);
            userContext.Team = jsonToken.Claims.First(w => w.Type.Equals("Team", StringComparison.InvariantCultureIgnoreCase)).Value;
            userContext.Info = jsonToken.Claims.First(w => w.Type.Equals("Info", StringComparison.InvariantCultureIgnoreCase)).Value;
            userContext.Number = jsonToken.Claims.First(w => w.Type.Equals("Number", StringComparison.InvariantCultureIgnoreCase)).Value;
        }

        await _next(context);
    }
}
