using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface ITeamService
{
    Task<Result<IEnumerable<TeamResponse>>> GetAsync(CancellationToken cancellationToken);
}