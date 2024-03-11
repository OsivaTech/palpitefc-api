using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Interfaces;

public interface IVotesService
{
    Task<VoteResponse> CreateAsync(VoteRequest request, CancellationToken cancellationToken);
    Task<VoteResponse> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<VoteResponse>> GetAsync(CancellationToken cancellationToken);
}
