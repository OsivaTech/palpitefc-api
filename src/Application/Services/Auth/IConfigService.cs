using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities;

namespace PalpiteApi.Application.Services.Auth;

public interface IConfigService
{
    Task<ConfigResponse> CreateOrUpdateAsync(ConfigRequest request);
    Task<Config> GetAsync(string name);
}
