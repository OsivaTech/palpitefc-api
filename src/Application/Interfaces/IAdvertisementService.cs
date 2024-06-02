using PalpiteFC.Api.Application.Requests.Auth;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteFC.Api.Application.Interfaces;
public interface IAdvertisementService
{
    Task<Result<AdvertisementResponse>> Create(AdvertisementRequest request, CancellationToken cancellationToken);
    Task<Result<IEnumerable<AdvertisementResponse>>> Get(CancellationToken cancellationToken);
    Task<Result<AdvertisementResponse>> Update(int id, AdvertisementRequest request, CancellationToken cancellationToken);

}
