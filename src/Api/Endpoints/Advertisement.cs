using FluentValidation;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Services;
using PalpiteFC.Api.Extensions;
using PalpiteFC.Api.Filters;

namespace PalpiteFC.Api.Endpoints;

public static class Advertisement
{
    public static void MapAdvertisementEndpoints(this WebApplication app)
    {

        app.MapPost("/advertisement", CreateAsync)
            .AddEndpointFilter<ValidationFilter<AdvertisementRequest>>()
            .WithSummary("Register a new advertisement.")
            .WithOpenApi();

        app.MapGet("/advertisement", GetAsync)
            .WithSummary("Get an advertisement list")
            .WithOpenApi();
        app.MapPut("/advertisement/{id}", UpdateAsync)
            .AddEndpointFilter<ValidationFilter<AdvertisementRequest>>()
            .WithSummary("Update an advertisemnt")
            .WithOpenApi();
    }
    private async static Task<IResult> CreateAsync(AdvertisementRequest request, IAdvertisementService service, CancellationToken cancellationToken)
    {
        var result = await service.Create(request, cancellationToken);
        return result.ToIResult(StatusCodes.Status201Created);
    }

    private async static Task<IResult> GetAsync(bool? details, IAdvertisementService service, CancellationToken cancellationToken)
    {
        var result = await service.Get(cancellationToken);
        return result.ToIResult();
    }
    private async static Task<IResult> UpdateAsync(int id, AdvertisementRequest request, IAdvertisementService service, CancellationToken cancellationToken)
    {
        var result = await service.Update(id, request, cancellationToken);
        return result.ToIResult();
    }
}
