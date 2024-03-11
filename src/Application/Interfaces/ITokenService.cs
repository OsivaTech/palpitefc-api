using PalpiteApi.Domain.Entities.Database;

namespace PalpiteApi.Application.Interfaces;

public interface ITokenService
{
    string Generate(Users user);
}
