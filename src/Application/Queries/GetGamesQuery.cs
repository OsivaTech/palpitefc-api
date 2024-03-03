using MediatR;
using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Queries.Handlers;

public class GetGamesQuery : IRequest<IEnumerable<GameResponse>> { }