﻿using Mapster;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository userRepository)
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
        await _repository.Update(request.Adapt<Users>());

        var user = await _repository.Select(request.Id);

        return ResultHelper.Success(user.Adapt<UserResponse>());
    }

    public async Task<Result<UserResponse>> GetByEmail(string email)
    {
        var user = await _repository.SelectByEmail(email);

        return ResultHelper.Success(user.Adapt<UserResponse>());
    }
}
