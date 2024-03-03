using MediatR;
using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Queries;

public class GetChampionshipQuery : IRequest<IEnumerable<ChampionshipResponse>> { }