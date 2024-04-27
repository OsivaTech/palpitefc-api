using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class User
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", async (IUserService service, CancellationToken cancellationToken) =>
        {
            var result = await service.GetAllAsync(cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization("admin");

        app.MapGet("/users/me", async (UserContext userContext) =>
        {
            var result = await Task.FromResult(ResultHelper.Success(userContext));

            return result.ToIResult();
        }).RequireAuthorization();

        app.MapPost("/users", async (UserRequest request, IUserService service, CancellationToken cancellationToken) =>
        {
            var result = await service.UpdateAsync(request, cancellationToken);

            return result.ToIResult();
        }).RequireAuthorization();
    }
}
