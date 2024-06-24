using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.Application.Responses;
using PalpiteFC.Api.Application.Utils;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.Integrations.Interfaces;

namespace PalpiteFC.Api.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IMercadoPagoProvider _mercadoPagoProvider;
    private readonly UserContext _userContext;

    public SubscriptionService(IMercadoPagoProvider mercadoPagoProvider, UserContext userContext)
    {
        _mercadoPagoProvider = mercadoPagoProvider;
        _userContext = userContext;
    }

    public async Task<Result<SubscriptionResponse>> SubscribeAsync(SubscriptionRequest request, CancellationToken cancellationToken)
    {
        var response = await _mercadoPagoProvider.CreatePreapproval(new()
        {
            BackUrl = "https://palpitefutebolclube.com/success",
            CardTokenId = request.CardTokenId,
            PayerEmail = _userContext.Email,
            PreapprovalPlanId = "2c93808490072837019040653b950eba"
        });

        return await Task.FromResult(ResultHelper.Success<SubscriptionResponse>(new()));
    }
}