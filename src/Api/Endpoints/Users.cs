using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.Extensions;
using PalpiteFC.Api.Filters;

namespace PalpiteFC.Api.Endpoints;

public static class User
{
    #region Public Methods

    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", GetAllAsync)
           .Produces<IEnumerable<UserResponse>>()
           .RequireAuthorization("admin")
           .WithSummary("Get all users.")
           .WithOpenApi();

        app.MapGet("/users/me", GetCurrentAsync)
           .Produces<UserResponse>()
           .RequireAuthorization()
           .WithSummary("Get the current logged in user.")
           .WithOpenApi();

        app.MapPost("/users", UpdateAsync)
           .Produces<UserResponse>()
           .RequireAuthorization()
           .AddEndpointFilter<ValidationFilter<UserRequest>>()
           .WithSummary("Update a user.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAllAsync(IUserService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> GetCurrentAsync(IUserService service, UserContext userContext, CancellationToken cancellationToken)
    {
        var result = await service.GetByEmail(userContext.Email, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> UpdateAsync(UserRequest request, IUserService service, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(request, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}

