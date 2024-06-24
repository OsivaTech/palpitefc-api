using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Integrations.MercadoPago.Requests;

public class PreapprovalRequest
{
    [JsonPropertyName("auto_recurring")]
    public AutoRecurring? AutoRecurring { get; set; }

    [JsonPropertyName("back_url")]
    public string? BackUrl { get; set; }

    [JsonPropertyName("card_token_id")]
    public string? CardTokenId { get; set; }

    [JsonPropertyName("external_reference")]
    public string? ExternalReference { get; set; }

    [JsonPropertyName("payer_email")]
    public string? PayerEmail { get; set; }

    [JsonPropertyName("preapproval_plan_id")]
    public string? PreapprovalPlanId { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

public class AutoRecurring
{
    [JsonPropertyName("frequency")]
    public int? Frequency { get; set; }

    [JsonPropertyName("frequency_type")]
    public string? FrequencyType { get; set; }

    [JsonPropertyName("start_date")]
    public DateTime? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("transaction_amount")]
    public decimal? TransactionAmount { get; set; }

    [JsonPropertyName("currency_id")]
    public string? CurrencyId { get; set; }
}