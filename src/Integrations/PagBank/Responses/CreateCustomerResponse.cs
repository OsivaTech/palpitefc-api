using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Integrations.PagBank.Responses;

public class CreateCustomerResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("tax_id")]
    public string? TaxId { get; set; }

    [JsonPropertyName("phones")]
    public Phone[]? Phones { get; set; }

    [JsonPropertyName("billing_info")]
    public BillingInfo[]? BillingInfo { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("links")]
    public Link[]? Links { get; set; }

    public class Link
    {
        [JsonPropertyName("rel")]
        public string? Rel { get; set; }

        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("media")]
        public string? Media { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}

public class BillingInfo
{
    public class Card
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("first_digits")]
        public string? FirstDigits { get; set; }

        [JsonPropertyName("last_digits")]
        public string? LastDigits { get; set; }

        [JsonPropertyName("exp_month")]
        public string? ExpirationMonth { get; set; }

        [JsonPropertyName("exp_year")]
        public string? ExpirationYear { get; set; }

        public class Holder
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }

        [JsonPropertyName("holder")]
        public Holder? CardHolder { get; set; }
    }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("card")]
    public Card? CardInfo { get; set; }
}

public class Phone
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("area")]
    public string? Area { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }
}