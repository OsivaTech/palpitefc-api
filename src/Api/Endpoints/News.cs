using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class News
{
    #region Public Methods

    public static void MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/news", GetAsync)
           .Produces<IEnumerable<NewsResponse>>()
           .WithSummary("Get all news.")
           .WithOpenApi();

        app.MapPost("/news", CreateAsync)
           .Produces<NewsResponse>(StatusCodes.Status201Created)
           .RequireAuthorization("admin", "journalist")
           .WithSummary("Create a news.")
           .WithOpenApi();

        app.MapPut("/news/{id}", UpdateAsync)
           .Produces<NewsResponse>()
           .RequireAuthorization("admin", "journalist")
           .WithSummary("Update a news.")
           .WithOpenApi();

        app.MapDelete("/news/{id}", DeleteAsync)
           .Produces(StatusCodes.Status204NoContent)
           .RequireAuthorization("admin", "journalist")
           .WithSummary("Delete a news.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetAsync(INewsService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> CreateAsync(NewsRequest request, INewsService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(request, cancellationToken);

        return result.ToIResult(201);
    }

    private async static Task<IResult> UpdateAsync(int id, NewsRequest request, INewsService service, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(id, request, cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> DeleteAsync(int id, INewsService service, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}

