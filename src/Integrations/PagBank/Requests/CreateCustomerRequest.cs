using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Integrations.PagBank.Requests;

public class CreateCustomerRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("tax_id")]
    public string? TaxId { get; set; }

    [JsonPropertyName("phones")]
    public Phone[]? Phones { get; set; }

    [JsonPropertyName("billing_info")]
    public BillingInfo[]? BillingInfo { get; set; }
}

public class BillingInfo
{
    public class Card
    {
        [JsonPropertyName("encrypted")]
        public string? Encrypted { get; set; }

        [JsonPropertyName("security_code")]
        public int? SecurityCode { get; set; }
    }

    [JsonPropertyName("card")]
    public Card? CardInfo { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public class Phone
{
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("area")]
    public string? Area { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }
}