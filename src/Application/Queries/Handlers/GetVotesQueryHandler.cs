using Mapster;
using MediatR;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Queries.Handlers;
public class GetVotesQueryHandler : IRequestHandler<GetVotesQuery, IEnumerable<VoteResponse>>
{
    #region Fields

    private readonly IVoteRepository _repository;

    #endregion

    #region Constructors

    public GetVotesQueryHandler(IVoteRepository repository)
    {
        _repository = repository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<VoteResponse>> Handle(GetVotesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.SelectWithOptions();

        return result.Adapt<IEnumerable<VoteResponse>>();
    }

    #endregion
}
