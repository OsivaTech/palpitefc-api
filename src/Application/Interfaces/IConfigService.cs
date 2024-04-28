using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IConfigService
{
    Task<Result<ConfigResponse>> CreateOrUpdateAsync(ConfigRequest request);
    Task<Result<Config>> GetAsync(string name);
}
