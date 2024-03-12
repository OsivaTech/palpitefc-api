using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IConfigService
{
    Task<Result<ConfigResponse>> CreateOrUpdateAsync(ConfigRequest request);
    Task<Result<Config>> GetAsync(string name);
}
