using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IPollService
{
    Task<Result<PollResponse>> CreateAsync(PollRequest request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<PollResponse>>> GetAsync(CancellationToken cancellationToken);
    Task<Result<PollResponse>> ComputeVoteAsync(int pollId, int optionId, CancellationToken cancellationToken);
}
