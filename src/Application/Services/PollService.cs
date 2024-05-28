using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Connection;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class PollService : IPollService
{
    #region Fields

    private readonly IPollsRepository _pollsRepository;
    private readonly IOptionsRepository _optionsRepository;
    private readonly IUserVotesRepository _userVotesRepository;
    private readonly UserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region Constructors

    public PollService(IPollsRepository repository,
                       IOptionsRepository optionsRepository,
                       IUserVotesRepository userVoteRepository,
                       UserContext userContext,
                       IUnitOfWork unitOfWork)
    {
        _pollsRepository = repository;
        _optionsRepository = optionsRepository;
        _userVotesRepository = userVoteRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Public Methods

    public async Task<Result<IEnumerable<PollResponse>>> GetAsync(CancellationToken cancellationToken)
    {
        var polls = await _pollsRepository.Select();

        var pollsResponse = polls.Adapt<IEnumerable<PollResponse>>().ToList();

        if (_userContext.Id > 0)
        {
            foreach (var poll in pollsResponse)
            {
                var userVote = await _userVotesRepository.SelectByPollIdAndUserId(poll.Id, _userContext.Id);

                if (userVote != null)
                {
                    poll.YourVote = userVote.OptionId;
                }
            }
        }

        return ResultHelper.Success<IEnumerable<PollResponse>>(pollsResponse);
    }

    public async Task<Result<PollResponse>> CreateAsync(PollRequest request, CancellationToken cancellationToken)
    {
        _unitOfWork.BeginTransaction();

        var id = await _pollsRepository.InsertAndGetId(request.Adapt<Poll>());

        cancellationToken.ThrowIfCancellationRequested();

        await _optionsRepository.Insert(request.Options!.Select(option => new Option() { PollId = id, Title = option.Title }));

        cancellationToken.ThrowIfCancellationRequested();

        var poll = await _pollsRepository.Select(id);

        _unitOfWork.Commit();

        return ResultHelper.Success(poll.Adapt<PollResponse>());
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _pollsRepository.Delete(id);

        return ResultHelper.Success();
    }

    public async Task<Result<PollResponse>> ComputeVoteAsync(int pollId, int optionId, CancellationToken cancellationToken)
    {
        _unitOfWork.BeginTransaction();

        var poll = await _pollsRepository.Select(pollId);

        cancellationToken.ThrowIfCancellationRequested();

        if (poll is null)
        {
            return ResultHelper.Failure<PollResponse>(PollErros.PollNotFound);
        }

        var userVote = await _userVotesRepository.SelectByPollIdAndUserId(pollId, _userContext.Id);

        cancellationToken.ThrowIfCancellationRequested();

        if (userVote is not null)
        {
            return ResultHelper.Failure<PollResponse>(PollErros.UserAlreadyVoted);
        }

        var option = await _optionsRepository.Select(optionId, pollId);

        cancellationToken.ThrowIfCancellationRequested();

        if (option is null)
        {
            return ResultHelper.Failure<PollResponse>(OptionErros.OptionNotFound);
        }

        await _optionsRepository.AddVote(optionId);

        cancellationToken.ThrowIfCancellationRequested();

        var userVoteEntity = new UserVote()
        {
            PollId = pollId,
            OptionId = optionId,
            UserId = _userContext.Id
        };

        await _userVotesRepository.Insert(userVoteEntity);

        cancellationToken.ThrowIfCancellationRequested();

        var updatedPoll = await _pollsRepository.Select(pollId);

        _unitOfWork.Commit();

        return ResultHelper.Success(updatedPoll.Adapt<PollResponse>());
    }

    #endregion
}
