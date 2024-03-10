using Mapster;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Services;
public class VotesService : IVotesService
{
    #region Fields

    private readonly IVotesRepository _votesRepository;
    private readonly IOptionsRepository _optionsRepository;

    #endregion

    #region Constructors

    public VotesService(IVotesRepository repository, IOptionsRepository optionsRepository)
    {
        _votesRepository = repository;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region Public Methods

    public async Task<IEnumerable<VoteResponse>> GetAsync(CancellationToken cancellationToken)
    {
        var votes = await _votesRepository.Select();
        var options = await _optionsRepository.Select();

        var voteResponse = new List<VoteResponse>();

        foreach (var vote in votes)
        {
            voteResponse.Add((options.Where(w => w.VoteId == vote.Id), vote).Adapt<VoteResponse>());
        }

        return voteResponse;
    }
    public Task<VoteResponse> GetAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #endregion
}
