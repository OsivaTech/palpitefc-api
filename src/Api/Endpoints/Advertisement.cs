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

        app.MapPost("/advertisement", CreateAd)
        .AddEndpointFilter<ValidationFilter<AdvertisementRequest>>()
        .WithSummary("Register a new advertisement.")
        .WithOpenApi();

        app.MapGet("/advertisement", GetAds)
            .WithSummary("Get an advertisement list")
            .WithOpenApi();
    }
    private async static Task<IResult> CreateAd(AdvertisementRequest request, IAdvertisementService service, CancellationToken cancellationToken)
    {
        var result = await service.Create(request, cancellationToken);
        return result.ToIResult(StatusCodes.Status201Created);
    }

    private async static Task<IResult> GetAds(bool? details, IAdvertisementService service, CancellationToken cancellationToken)
    {
        var result = await service.Get(cancellationToken);
        return result.ToIResult();
    }
}
