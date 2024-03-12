using PalpiteApi.Api.Extensions;
using PalpiteApi.Application;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Api.Endpoints.Authenticated;

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
