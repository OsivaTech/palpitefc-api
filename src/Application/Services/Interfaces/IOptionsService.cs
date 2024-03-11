using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services.Interfaces;

public interface IOptionsService
{
    Task<Result<VoteResponse>> ComputeVoteAsync(OptionsRequest request, CancellationToken cancellationToken);
    Task<Result<OptionsResponse>> CreateAsync(OptionsRequest request, CancellationToken cancellationToken);
}
