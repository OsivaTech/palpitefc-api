using Mapster;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities.Database;
using PalpiteApi.Domain.Interfaces.Database;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services;
public class ConfigService : IConfigService
{
    private readonly IConfigRepository _repository;

    public ConfigService(IConfigRepository repository)
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
