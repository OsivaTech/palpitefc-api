using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface ITokenService
{
    Result<string> Generate(Users user);
}
