using System.Text.Json;
using System.Text.Json.Serialization;

namespace PalpiteApi.Api.Converters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTime(out DateTime dateTime))
        {
            return dateTime;
        }

        throw new JsonException($"Failed to convert {reader.GetString()} to DateTime.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        string formattedDate = value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        writer.WriteStringValue(formattedDate);
    }
}
