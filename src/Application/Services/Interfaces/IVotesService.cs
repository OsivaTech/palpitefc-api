using PalpiteApi.Application.Responses;

namespace PalpiteApi.Application.Services.Interfaces;
public interface IVotesService
{
    Task<IEnumerable<VoteResponse>> GetAsync(CancellationToken cancellationToken);
}
