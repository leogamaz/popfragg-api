using System.Text.Json;
using System.Text.Json.Serialization;

public class GuidListConverter : JsonConverter<List<Guid>>
{
    public override List<Guid>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var guids = new List<Guid>();

        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                break;

            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (Guid.TryParse(stringValue, out var guid))
                    guids.Add(guid);
                else
                    throw new JsonException($"Invalid GUID: {stringValue}");
            }
        }

        return guids;
    }

    public override void Write(Utf8JsonWriter writer, List<Guid> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var guid in value)
            writer.WriteStringValue(guid.ToString());
        writer.WriteEndArray();
    }
}
