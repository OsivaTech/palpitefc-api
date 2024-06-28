using System.Text.Json.Serialization;

namespace PalpiteFC.Api.Integrations.PagBank.Responses;

public class ErrorResponse
{
    [JsonPropertyName("error_messages")]
    public ErrorMessage[]? ErrorMessages { get; set; }

    public class ErrorMessage
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("parameter_name")]
        public string? ParameterName { get; set; }
    }
}
