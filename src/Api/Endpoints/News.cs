using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Extensions;

namespace PalpiteFC.Api.Endpoints;

public static class News
{
    #region Public Methods

    public static void MapNewsEndpoints(this WebApplication app)
    {
        app.MapGet("/news", GetNews)
           .WithSummary("Get all news.")
           .WithOpenApi();

        app.MapPost("/news", CreateOrUpdateNews)
           .RequireAuthorization("admin", "journalist")
           .WithSummary("Create or update news.")
           .WithOpenApi();

        app.MapDelete("/news", DeleteNews)
           .RequireAuthorization("admin", "journalist")
           .WithSummary("Delete a news item.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> GetNews(INewsService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(cancellationToken);

        return result.ToIResult();
    }

    private async static Task<IResult> CreateOrUpdateNews(NewsRequest request, INewsService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateOrUpdateAsync(request);

        return result.ToIResult();
    }

    private async static Task<IResult> DeleteNews(int id, INewsService service, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken);

        return result.ToIResult();
    }

    #endregion
}

