using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<Users>
{
    Task<int> Exists(string email);
    Task<IEnumerable<Users>> FindByEmail(string email);
    Task<IEnumerable<Users>> SelectByPoints();
}
