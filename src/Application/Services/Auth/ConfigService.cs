using Mapster;
using PalpiteApi.Application.Requests.Auth;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Entities;
using PalpiteApi.Domain.Interfaces;

namespace PalpiteApi.Application.Services.Auth;
public class ConfigService : IConfigService
{
    private readonly IConfigRepository _repository;

    public ConfigService(IConfigRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConfigResponse> CreateOrUpdateAsync(ConfigRequest request)
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

        return request.Adapt<ConfigResponse>();
    }

    public async Task<Config> GetAsync(string name)
    {
        var configs = await _repository.Select(name);

        return configs.FirstOrDefault(new Config());
    }
}
