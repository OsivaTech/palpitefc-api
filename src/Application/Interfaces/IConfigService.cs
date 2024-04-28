using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IConfigService
{
    Task<Result<ConfigResponse>> GetAsync(string name);
    Task<Result<ConfigResponse>> CreateAsync(ConfigRequest request);
    Task<Result<ConfigResponse>> UpdateAsync(int id, ConfigRequest request);
    Task<Result> DeleteAsync(int id);
}
