using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<UserResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<UserResponse>> GetByEmail(string email);
    Task<Result<UserResponse>> UpdateAsync(UserRequest request, CancellationToken cancellationToken);
}