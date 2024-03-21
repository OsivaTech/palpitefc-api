using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface ITokenService
{
    Result<string> Generate(Users user);
}
