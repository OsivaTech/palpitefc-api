using PalpiteApi.Application.Requests;
using PalpiteApi.Domain.Result;

namespace PalpiteApi.Application.Services.Auth;

public interface IPalpitationService
{
    Task<Result> Create(PalpitationRequest request, CancellationToken cancellationToken);
}