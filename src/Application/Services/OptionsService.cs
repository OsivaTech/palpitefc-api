using Mapster;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Errors;
using PalpiteApi.Domain.Interfaces;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services;

public class OptionsService : IOptionsService
{
    #region Fields

    private readonly IOptionsRepository _optionsRepository;
    private readonly IVotesRepository _votesRepository;

    #endregion

    #region Constructors

    public OptionsService(IOptionsRepository optionsRepository, IVotesRepository votesRepository)
    {
        _optionsRepository = optionsRepository;
        _votesRepository = votesRepository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<OptionsResponse>> CreateAsync(OptionsRequest request, CancellationToken cancellationToken)
    {
        var id = await _optionsRepository.InsertAndGetId(request.Adapt<Options>());

        var option = await _optionsRepository.Select(id);

        return ResultHelper.Success(option.Adapt<OptionsResponse>());
    }

    public async Task<Result<VoteResponse>> ComputeVoteAsync(OptionsRequest request, CancellationToken cancellationToken)
    {
        var currentOption = await _optionsRepository.Select(request.Id);

        if (currentOption is null)
        {
            return ResultHelper.Failure<VoteResponse>(OptionsErros.OptionNotFound);
        }

        var updatedOption = new Options
        {
            Id = currentOption.Id,
            VoteId = currentOption.VoteId,
            Title = currentOption.Title,
            Count = currentOption.Count + 1,
        };

        await _optionsRepository.Update(updatedOption);

        var votes = await _votesRepository.Select(updatedOption.VoteId);
        var options = await _optionsRepository.SelectByVoteId(updatedOption.VoteId);

        var response = new VoteResponse
        {
            Id = votes.Id,
            Title = votes.Title,
            Options = options.Where(w => w.VoteId == votes.Id).Adapt<IEnumerable<OptionsResponse>>()
        };

        return ResultHelper.Success(response);
    }

    #endregion
}
