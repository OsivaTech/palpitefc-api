using PalpiteFC.Api.Integrations.MercadoPago.Requests;
using PalpiteFC.Api.Integrations.MercadoPago.Responses;

namespace PalpiteFC.Api.Integrations.Interfaces;
public interface IMercadoPagoProvider
{
    Task<PreapprovalResponse?> CreatePreapproval(PreapprovalRequest request);
}