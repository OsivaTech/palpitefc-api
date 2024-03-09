using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Application.Services.Interfaces;

public interface ITokenService
{
    string Generate(Users user);
}
