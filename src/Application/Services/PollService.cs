using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class PollService : IPollService
{
    #region Fields

    private readonly IPollsRepository _pollsRepository;
    private readonly IOptionsRepository _optionsRepository;

    #endregion

    #region Constructors

    public PollService(IPollsRepository repository, IOptionsRepository optionsRepository)
    {
        _pollsRepository = repository;
        _optionsRepository = optionsRepository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<PollResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var votes = await _pollsRepository.Select();
        var options = await _optionsRepository.Select();

        var voteResponse = new List<PollResponse>();

        foreach (var vote in votes)
        {
            voteResponse.Add((vote, options.Where(w => w.VoteId == vote.Id)).Adapt<PollResponse>());
        }

        return ResultHelper.Success<IEnumerable<PollResponse>>(voteResponse);
    }

    public async Task<Result<PollResponse>> CreateAsync(PollRequest request, CancellationToken cancellationToken)
    {
        var id = await _pollsRepository.InsertAndGetId(request.Adapt<Poll>());

        var vote = await _pollsRepository.Select(id);

        return ResultHelper.Success(vote.Adapt<PollResponse>());
    }

    public async Task<Result<PollResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _pollsRepository.Delete(id);

        return ResultHelper.Success<PollResponse>(new() { Id = id });
    }

    #endregion
}
