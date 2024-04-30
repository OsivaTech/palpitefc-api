using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class OptionsService : IOptionsService
{
    #region Fields

    private readonly IOptionsRepository _optionsRepository;
    private readonly IPollsRepository _pollsRepository;

    #endregion

    #region Constructors

    public OptionsService(IOptionsRepository optionsRepository, IPollsRepository pollsRepository)
    {
        _optionsRepository = optionsRepository;
        _pollsRepository = pollsRepository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<OptionsResponse>> CreateAsync(OptionsRequest request, CancellationToken cancellationToken)
    {
        var id = await _optionsRepository.InsertAndGetId(request.Adapt<Libraries.Persistence.Abstractions.Entities.Option>());

        var option = await _optionsRepository.Select(id);

        return ResultHelper.Success(option.Adapt<OptionsResponse>());
    }

    public async Task<Result<PollResponse>> ComputeVoteAsync(OptionsRequest request, CancellationToken cancellationToken)
    {
        var currentOption = await _optionsRepository.Select(request.Id);

        if (currentOption is null)
        {
            return ResultHelper.Failure<PollResponse>(OptionsErros.OptionNotFound);
        }

        var updatedOption = new Libraries.Persistence.Abstractions.Entities.Option
        {
            Id = currentOption.Id,
            PollId = currentOption.PollId,
            Title = currentOption.Title,
            Count = currentOption.Count + 1,
        };

        await _optionsRepository.Update(updatedOption);

        var poll = await _pollsRepository.Select(updatedOption.PollId);
        var options = await _optionsRepository.SelectByPollId(updatedOption.PollId);

        var response = new PollResponse
        {
            Id = poll.Id,
            Title = poll.Title,
            Options = options.Where(w => w.PollId == poll.Id).Adapt<IEnumerable<OptionsResponse>>()
        };

        return ResultHelper.Success(response);
    }

    #endregion
}
