using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class UserAddresses
{
    #region Public Methods

    public static void MapUserAddressEndpoints(this WebApplication app)
    {
        app.MapGet("/userAddresses/me", GetCurrentAsync)
           .Produces<IEnumerable<AddressResponse>>()
           .RequireAuthorization()
           .WithSummary("Get addresses from the current user.")
           .WithOpenApi();

        app.MapPost("/userAddresses", CreateAsync)
           .Produces<AddressResponse>()
           .RequireAuthorization()
           .WithSummary("Create new address for the current user.")
           .WithOpenApi();

        app.MapPut("/userAddresses/{id}", UpdateAsync)
           .Produces<AddressResponse>()
           .RequireAuthorization()
           .WithSummary("Update address of the current user.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private static async Task<IResult> GetCurrentAsync(IUserAddressService service, CancellationToken cancellationToken)
    {
        var result = await service.GetCurrentAsync(cancellationToken);

        return result.ToIResult();
    }

    private static async Task<IResult> CreateAsync(AddressRequest request, IUserAddressService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return result.ToIResult();
    }

    private static async Task<IResult> UpdateAsync(int id, AddressRequest request, IUserAddressService service, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(id, request, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}