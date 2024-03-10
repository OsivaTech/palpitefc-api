using Mapster;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Application.Services.Interfaces;
using PalpiteApi.Domain.Errors;
using PalpiteApi.Domain.Interfaces;
using PalpiteApi.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteApi.Application.Services;
public class OptionsService : IOptionsService
{
    private readonly IOptionsRepository _optionsRepository;
    private readonly IVotesRepository _votesRepository;

    public OptionsService(IOptionsRepository optionsRepository, IVotesRepository votesRepository)
    {
        _optionsRepository = optionsRepository;
        _votesRepository = votesRepository;
    }
    public async Task<Result<VoteResponse>> SendOption(OptionsRequest req, CancellationToken cancellationToken)
    {

        if (req.Id <= 0) 
        {
            return Result.Failure<VoteResponse>(OptionsErros.MissingParams);
        }

        var option = await _optionsRepository.Select(req.Id);

        if (option is null)
        {
            return Result.Failure<VoteResponse>(OptionsErros.OptionNotFound);
        }

        var options = await _optionsRepository.SelectByVoteId(option.VoteId);

        if (options is null)
        {
            return Result.Failure<VoteResponse>(OptionsErros.OptionNotFound);
        }

        var votes = await _votesRepository.Select(option.VoteId);

        if (votes == null)
        {
            return Result.Failure<VoteResponse>(OptionsErros.EnqueteNotFound);
        }

        await _optionsRepository.AddVote(option!.Count + 1, option);


        var response = new VoteResponse
        {
            Id = votes.Id,
            Title = votes.Title,
            Options = options.Adapt<IEnumerable<OptionsResponse>>()

        };


        return Result.Success(response);
    }
}
