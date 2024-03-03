using MediatR;
using PalpiteApi.Application.Queries.Handlers;

namespace PalpiteApi.Api.Endpoints;

public static class Game
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        app.MapGet("/game", (IMediator mediator) => mediator.Send(new GetGamesQuery()) );
    }
}
