using System.Text.Json;
using System.Text.Json.Serialization;

namespace SetmoreSharp.Converters
{
    public class DecimalConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            // Cater for converting a string to a decimal
            if (reader.TokenType == JsonTokenType.String)
            {
                var strValue = reader.GetString();

                if (decimal.TryParse(strValue, out var value))
                {
                    return value;
                }
            }

            // We should have a valid number/decimal at this point, so do the conversion directly
            return reader.GetDecimal();
        }

        public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException("Should not get here.");
        }
    }
}