using PalpiteFC.Api.Domain.Entities.Database;

namespace PalpiteFC.Api.Domain.Interfaces.Database;

public interface IUserRepository : IBaseRepository<Users>
{
    Task<Users?> SelectByEmail(string email);
    Task UpdatePassword(string email, string password);
}
