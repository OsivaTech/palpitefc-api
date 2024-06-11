using Mapster;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Libraries.Persistence.Abstractions.Connection;
using PalpiteFC.Libraries.Persistence.Abstractions.Entities;
using PalpiteFC.Libraries.Persistence.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalpiteFC.Api.Application.Services;
public class WaitingListService : IWaitingListService
{
    private readonly IWaitingListRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WaitingListService(IWaitingListRepository waitingListRepository)
    {
        _repository = waitingListRepository;
    }
    public async Task<Result<IEnumerable<WaitingListResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _repository.Select();

        return ResultHelper.Success(result.Adapt<IEnumerable<WaitingListResponse>>());
    }

    public async Task<Result> InsertAsync(WaitingListRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.CheckIfEmailExists(request.Email!) is true)
        {
            return ResultHelper.Failure<WaitingListResponse>(SignUpErrors.EmailAlreadyUsed);
        }

        await _repository.Insert(request.Adapt<WaitingList>());


        return ResultHelper.Success();
    }
}
