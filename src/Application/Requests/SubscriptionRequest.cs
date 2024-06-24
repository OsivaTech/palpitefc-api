using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Application.Requests;

public class SubscriptionRequest
{
    [JsonPropertyName("token")]
    public string? CardTokenId { get; set; }
}