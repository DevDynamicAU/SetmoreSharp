using System.Text.Json;
using System.Text.Json.Serialization;

namespace SetmoreSharp.Converters
{
    public class DoubleConverter : JsonConverter<double?>
    {
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var strValue = reader.GetString();

                if (double.TryParse(strValue, out var value))
                {
                    return value;
                }
            }

            return reader.GetDouble();
        }

        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException("Should not get here.");
        }
    }
}