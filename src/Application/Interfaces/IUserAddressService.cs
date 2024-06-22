using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IUserAddressService
{
    Task<Result<AddressResponse>> GetCurrentAsync(CancellationToken cancellationToken);
    Task<Result<AddressResponse>> CreateAsync(AddressRequest request, CancellationToken cancellationToken);
    Task<Result<AddressResponse>> UpdateAsync(int id, AddressRequest request, CancellationToken cancellationToken);
}