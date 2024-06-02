using Mapster;
using Microsoft.Extensions.Options;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.CrossCutting.Settings;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteFC.Api.Application.Services;
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    //private readonly IOptions<AdvertisementSettings> _settings;

    //cadastra anuncio no banco de dados
    public AdvertisementService(IAdvertisementRepository advertisementRepository)
    {
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<AdvertisementResponse>> Create(AdvertisementRequest request, CancellationToken cancellationToken)
    {
        await _advertisementRepository.Insert(request.Adapt<Advertisement>());

        return ResultHelper.Success(request.Adapt<AdvertisementResponse>());
    }
    public async Task<Result<IEnumerable<AdvertisementResponse>>> Get(CancellationToken cancellationToken)
    {
       var result =  await _advertisementRepository.SelectToday();
        return ResultHelper.Success(result.Adapt<IEnumerable<AdvertisementResponse>>());
    }

    public async Task<Result<AdvertisementResponse>> Update(int id, AdvertisementRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Advertisement>();
        entity.Id = id;

        await _advertisementRepository.Update(entity);
        return ResultHelper.Success(entity.Adapt<AdvertisementResponse>());
    }
}
