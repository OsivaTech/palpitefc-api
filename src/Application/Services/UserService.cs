using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class UserService : IUserService
{
    private readonly IUsersRepository _repository;

    public UserService(IUsersRepository userRepository)
    {
        _repository = userRepository;
    }

    public async Task<Result<IEnumerable<UserResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.Select();

        return ResultHelper.Success(result.Adapt<IEnumerable<UserResponse>>());
    }

    public async Task<Result<UserResponse>> UpdateAsync(UserRequest request, CancellationToken cancellationToken)
    {
        await _repository.Update(request.Adapt<User>());

        var user = await _repository.Select(request.Id);

        return ResultHelper.Success(user.Adapt<UserResponse>());
    }

    public async Task<Result<UserResponse>> GetByEmail(string email)
    {
        var user = await _repository.SelectByEmail(email);

        return ResultHelper.Success(user.Adapt<UserResponse>());
    }
}
