using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Errors;
using PalpiteFC.Api.Domain.Interfaces.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Services;
public class RankingService : IRankingService
{
    private readonly IUserRepository _userRepository;
    public RankingService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserResponse>>> GetAsync(CancellationToken cancellationToken)
    {

        var users = await _userRepository.SelectByPoints();

        if (users == null)
        {
            return ResultHelper.Failure<IEnumerable<UserResponse>>(RankingErrors.PointNotFound);
        }

        return ResultHelper.Success(users.Adapt<IEnumerable<UserResponse>>());

    }

}
