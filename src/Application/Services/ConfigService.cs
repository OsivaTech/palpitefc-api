using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Errors;
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

    public async Task<Result<ConfigResponse>> CreateAsync(ConfigRequest request)
    {
        var configs = await _repository.Select(request.Name!);

        if (configs.Any())
        {
            return ResultHelper.Failure<ConfigResponse>(ConfigErrors.AlreadyExists);
        }

        await _repository.Insert(request.Adapt<Config>());

        configs = await _repository.Select(request.Name!);

        return ResultHelper.Success(configs.FirstOrDefault().Adapt<ConfigResponse>());
    }

    public async Task<Result> DeleteAsync(int id)
    {
        await _repository.Delete(id);

        return ResultHelper.Success();
    }

    public async Task<Result<ConfigResponse>> GetAsync(string name)
    {
        var configs = await _repository.Select(name);

        return ResultHelper.Success(configs.FirstOrDefault().Adapt<ConfigResponse>());
    }

    public async Task<Result<ConfigResponse>> UpdateAsync(int id, ConfigRequest request)
    {
        var configs = await _repository.Select(request.Name!);

        if (configs is null)
        {
            return ResultHelper.Failure<ConfigResponse>(ConfigErrors.NotExists);
        }

        await _repository.Update(request.Adapt<Config>());

        throw new NotImplementedException();
    }
}
