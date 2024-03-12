using PalpiteApi.Application.Requests;
using PalpiteApi.Application.Responses;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Interfaces;

public interface IPalpitationService
{
    Task<Result<PalpitationResponse>> Create(PalpitationRequest request, CancellationToken cancellationToken);
}