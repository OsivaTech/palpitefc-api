using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;

namespace PalpiteFC.Api.Application.Interfaces;

public interface ITokenService
{
    Result<string> Generate(User user);
}
