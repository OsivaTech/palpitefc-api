using MediatR;
using PalpiteApi.Application.Queries;

namespace PalpiteApi.Api.Endpoints;

public static class Championship
{
    public static void MapChampionshipEndpoints(this WebApplication app)
    {
        app.MapGet("/championship", (IMediator mediator) => mediator.Send(new GetChampionshipQuery()));
    }
}
