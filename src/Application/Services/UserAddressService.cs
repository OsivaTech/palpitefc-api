using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Connection;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;

namespace PalpiteFC.Api.Application.Services;

public class UserAddressService : IUserAddressService
{
    #region Fields

    private readonly IUsersAddressesRepository _repository;
    private readonly IUsersRepository _usersRepository;
    private readonly UserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region Constructor

    public UserAddressService(IUsersAddressesRepository repository, IUsersRepository usersRepository, UserContext userContext, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _userContext = userContext;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Public Methods

    public async Task<Result<AddressResponse>> CreateAsync(AddressRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContext.Id;

        _unitOfWork.BeginTransaction();

        var userAddresses = await _repository.SelectByUserId(userId);

        cancellationToken.ThrowIfCancellationRequested();

        if (userAddresses is not null)
        {
            return ResultHelper.Failure<AddressResponse>(UserAddressErrors.ExistingAddress);
        }

        var addressId = await _repository.InsertAndGetId(request.Adapt<UserAddress>());

        await _usersRepository.Update(new() { Id = userId, AddressId = addressId });

        cancellationToken.ThrowIfCancellationRequested();

        var address = await _repository.Select(addressId);

        _unitOfWork.Commit();

        return ResultHelper.Success(address.Adapt<AddressResponse>());
    }

    public async Task<Result<AddressResponse>> GetCurrentAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.SelectByUserId(_userContext.Id);

        return ResultHelper.Success(result.Adapt<AddressResponse>());
    }

    public async Task<Result<AddressResponse>> UpdateAsync(int id, AddressRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<UserAddress>();
        entity.Id = id;

        await _repository.Update(entity);

        var address = await _repository.Select(id);

        return ResultHelper.Success(address.Adapt<AddressResponse>());
    }

    #endregion
}