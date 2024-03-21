using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IVotesService
{
    Task<Result<VoteResponse>> CreateAsync(VoteRequest request, CancellationToken cancellationToken);
    Task<Result<VoteResponse>> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<VoteResponse>>> GetAsync(CancellationToken cancellationToken);
}
