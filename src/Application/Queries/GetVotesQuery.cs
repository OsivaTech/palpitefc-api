using MediatR;
using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Queries;

public class GetVotesQuery : IRequest<IEnumerable<VoteResponse>> { }
