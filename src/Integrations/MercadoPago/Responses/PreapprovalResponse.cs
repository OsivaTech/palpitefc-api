using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Integrations.MercadoPago.Responses;

public class PreapprovalResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }

    [JsonPropertyName("application_id")]
    public long? ApplicationId { get; set; }

    [JsonPropertyName("collector_id")]
    public long? CollectorId { get; set; }

    [JsonPropertyName("preapproval_plan_id")]
    public string? PreapprovalPlanId { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("external_reference")]
    public long? ExternalReference { get; set; }

    [JsonPropertyName("back_url")]
    public string? BackUrl { get; set; }

    [JsonPropertyName("init_point")]
    public string? InitPoint { get; set; }

    [JsonPropertyName("auto_recurring")]
    public AutoRecurring? AutoRecurring { get; set; }

    [JsonPropertyName("payer_id")]
    public long? PayerId { get; set; }

    [JsonPropertyName("card_id")]
    public long? CardId { get; set; }

    [JsonPropertyName("payment_method_id")]
    public long? PaymentMethodId { get; set; }

    [JsonPropertyName("next_payment_date")]
    public DateTime? NextPaymentDate { get; set; }

    [JsonPropertyName("date_created")]
    public DateTime? DateCreated { get; set; }

    [JsonPropertyName("last_modified")]
    public DateTime? LastModified { get; set; }

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

    [JsonPropertyName("currency_id")]
    public string? CurrencyId { get; set; }

    [JsonPropertyName("transaction_amount")]
    public decimal? TransactionAmount { get; set; }

    [JsonPropertyName("free_trial")]
    public FreeTrial? FreeTrial { get; set; }
}

public class FreeTrial
{
    [JsonPropertyName("frequency")]
    public int? Frequency { get; set; }

    [JsonPropertyName("frequency_type")]
    public string? FrequencyType { get; set; }
}