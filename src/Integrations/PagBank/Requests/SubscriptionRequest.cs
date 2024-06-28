using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Integrations.PagBank.Requests;

public class SubscriptionRequest
{
    [JsonPropertyName("plan")]
    public Plan? Plan { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    [JsonPropertyName("payment_method")]
    public PaymentMethod[]? PaymentMethods { get; set; }
}

public class Plan
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public class Customer
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public class PaymentMethod
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("card")]
    public Card? CardInfo { get; set; }
}

public class Card
{
    [JsonPropertyName("security_code")]
    public int? SecurityCode { get; set; }
}