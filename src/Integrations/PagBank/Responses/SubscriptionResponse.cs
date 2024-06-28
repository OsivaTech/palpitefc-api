using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Integrations.PagBank.Responses;

public class SubscriptionResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("tax_id")]
    public string? TaxId { get; set; }

    [JsonPropertyName("phones")]
    public Phone[]? Phones { get; set; }

    [JsonPropertyName("birth_date")]
    public DateTimeOffset BirthDate { get; set; }

    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    [JsonPropertyName("billing_info")]
    public BillingInfo[]? BillingInfo { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("links")]
    public Link[]? Links { get; set; }
}

public class Address
{
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }

    [JsonPropertyName("complement")]
    public string? Complement { get; set; }

    [JsonPropertyName("locality")]
    public string? Locality { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("region_code")]
    public string? RegionCode { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("postal_code")]
    public string? PostalCode { get; set; }
}

public class Card
{
    [JsonPropertyName("security_code")]
    public int? SecurityCode { get; set; }
}

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