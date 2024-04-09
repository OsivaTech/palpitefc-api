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
    private readonly IPollsRepository _votesRepository;

    #endregion

    #region Constructors

    public OptionsService(IOptionsRepository optionsRepository, IPollsRepository votesRepository)
    {
        _optionsRepository = optionsRepository;
        _votesRepository = votesRepository;
    }

    #endregion

    #region Public Methods

    public async Task<Result<OptionsResponse>> CreateAsync(OptionsRequest request, CancellationToken cancellationToken)
    {
        var id = await _optionsRepository.InsertAndGetId(request.Adapt<Libraries.Persistence.Abstractions.Entities.Option>());

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

        var updatedOption = new Libraries.Persistence.Abstractions.Entities.Option
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
