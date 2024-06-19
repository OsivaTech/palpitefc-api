using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.CrossCutting.Result;

namespace PalpiteFC.Api.Application.Interfaces;

public interface IWaitingListService
{
    Task<Result<IEnumerable<WaitingListResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result> SendWelcomeAsync(IEnumerable<WelcomeRequest> request, CancellationToken cancellationToken);
    Task<Result> InsertAsync(WaitingListRequest request, CancellationToken cancellationToken);
}