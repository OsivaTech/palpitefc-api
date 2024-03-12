using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IVotesService
{
    Task<Result<VoteResponse>> CreateAsync(VoteRequest request, CancellationToken cancellationToken);
    Task<Result<VoteResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<VoteResponse>>> GetAsync(CancellationToken cancellationToken);
}
