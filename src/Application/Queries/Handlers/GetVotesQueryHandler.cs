using Mapster;
using MediatR;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Queries.Handlers;
public class GetVotesQueryHandler : IRequestHandler<GetVotesQuery, IEnumerable<VoteResponse>>
{
    #region Fields

    private readonly IVotesRepository _votesRepository;
    private readonly IOptionsRepository _OptionsRepository;

    #endregion

    #region Constructors

    public GetVotesQueryHandler(IVotesRepository repository, IOptionsRepository optionsRepository)
    {
        _votesRepository = repository;
        _OptionsRepository = optionsRepository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<VoteResponse>> Handle(GetVotesQuery request, CancellationToken cancellationToken)
    {
        var votes = await _votesRepository.Select();
        var options = await _OptionsRepository.Select();

        var voteResponse = new List<VoteResponse>();

        foreach (var vote in votes)
        {
            voteResponse.Add((options.Where(w => w.VoteId == vote.Id), vote).Adapt<VoteResponse>());
        }

        return voteResponse;
    }

    #endregion
}
