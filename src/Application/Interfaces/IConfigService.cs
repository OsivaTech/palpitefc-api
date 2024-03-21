using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Domain.Entities.Database;
using PalpiteFC.Api.Domain.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IConfigService
{
    Task<Result<ConfigResponse>> CreateOrUpdateAsync(ConfigRequest request);
    Task<Result<Config>> GetAsync(string name);
}
