using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<UserResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<UserResponse>> GetByEmail(string email);
    Task<Result<UserResponse>> UpdateAsync(UserRequest request, CancellationToken cancellationToken);
}