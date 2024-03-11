using Mapster;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Entities;
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
            voteResponse.Add((vote, options.Where(w => w.VoteId == vote.Id)).Adapt<VoteResponse>());
        }

        return voteResponse;
    }

    public async Task<VoteResponse> CreateAsync(VoteRequest request, CancellationToken cancellationToken)
    {
        var id = await _votesRepository.InsertAndGetId(request.Adapt<Votes>());

        var vote = await _votesRepository.Select(id);

        return vote.Adapt<VoteResponse>();
    }

    public async Task<VoteResponse> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _votesRepository.Delete(id);

        return new() { Id = id };
    }

    #endregion
}
