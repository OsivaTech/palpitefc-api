using PalpiteFC.Api.Integrations.Interfaces;
using PalpiteFC.Api.Integrations.MercadoPago.Requests;
using PalpiteFC.Api.Integrations.MercadoPago.Responses;
using System.Text.Json;

namespace PalpiteFC.Api.Integrations.MercadoPago;

public class MercadoPagoProvider : IMercadoPagoProvider
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public MercadoPagoProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PreapprovalResponse?> CreatePreapproval(PreapprovalRequest request)
    {
        var content = new StringContent(JsonSerializer.Serialize(request, _serializerOptions));

        var httpResponse = await _httpClient.PostAsync("preapproval", content);

        var responseString = await httpResponse.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<PreapprovalResponse>(responseString);
    }
}
