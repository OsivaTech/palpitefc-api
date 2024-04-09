using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;
public class ConfigService : IConfigService
{
    private readonly IConfigsRepository _repository;

    public ConfigService(IConfigsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ConfigResponse>> CreateOrUpdateAsync(ConfigRequest request)
    {
        var configs = await _repository.Select(request.Name!);

        if (configs.Any())
        {
            await _repository.Update(request.Adapt<Config>());
        }
        else
        {
            await _repository.Insert(request.Adapt<Config>());
        }

        return ResultHelper.Success(request.Adapt<ConfigResponse>());
    }

    public async Task<Result<Config>> GetAsync(string name)
    {
        var configs = await _repository.Select(name);

        return ResultHelper.Success(configs.FirstOrDefault(new Config()));
    }
}
