using Mapster;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Errors;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services;
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
