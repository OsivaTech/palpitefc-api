using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class VotesService : IVotesService
{
    #region Fields

    private readonly IPollsRepository _votesRepository;
    private readonly IOptionsRepository _optionsRepository;

    #endregion

    #region Constructors

    public VotesService(IPollsRepository repository, IOptionsRepository optionsRepository)
    {
        _votesRepository = repository;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<VoteResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var votes = await _votesRepository.Select();
        var options = await _optionsRepository.Select();

        var voteResponse = new List<VoteResponse>();

        foreach (var vote in votes)
        {
            voteResponse.Add((vote, options.Where(w => w.VoteId == vote.Id)).Adapt<VoteResponse>());
        }

        return ResultHelper.Success<IEnumerable<VoteResponse>>(voteResponse);
    }

    public async Task<Result<VoteResponse>> CreateAsync(VoteRequest request, CancellationToken cancellationToken)
    {
        var id = await _votesRepository.InsertAndGetId(request.Adapt<Poll>());

        var vote = await _votesRepository.Select(id);

        return ResultHelper.Success(vote.Adapt<VoteResponse>());
    }

    public async Task<Result<VoteResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _votesRepository.Delete(id);

        return ResultHelper.Success<VoteResponse>(new() { Id = id });
    }

    #endregion
}
