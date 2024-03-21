using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class User
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/user", async (IUserService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAllAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
