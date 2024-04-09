using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints.Authenticated;

public static class User
{
    public static void MapAuthUserEndpoints(this WebApplication app)
    {
        app.MapGet("/auth/user", async (UserContext userContext) =>
        {
            var result = await Task.FromResult(ResultHelper.Success(userContext));

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/auth/user", async (UserRequest request, IUserService service, CancellationToken cancellationToken) =>
        {
            var result = await service.UpdateAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
