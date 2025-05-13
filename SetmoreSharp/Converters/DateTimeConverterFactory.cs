using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SetmoreSharp.Converters
{
    public class DateTimeConverterFactory : JsonConverterFactory
    {
        private static readonly string[] _formats = new[]
        {
            "dd/MM/yyyy",
            "yyyy/MM/dd"
        };

        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(DateTime) || typeToConvert == typeof(DateTime?);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(DateTime))
            {
                return new NonNullableConverter(_formats);
            }
            else
            { // DateTime?
                return new NullableConverter(_formats);
            }
        }

        private class NonNullableConverter : JsonConverter<DateTime>
        {
            private readonly string[] _formats;

            public NonNullableConverter(string[] formats) => _formats = formats;

            public override DateTime Read(ref Utf8JsonReader reader, Type _, JsonSerializerOptions __)
            {
                if (reader.TokenType != JsonTokenType.String) return default;

                var success = DateTime.TryParseExact(reader.GetString(), _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt);

                var result = success ? dt : reader.GetDateTime();

                return result;
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions __)
            {
                writer.WriteStringValue(value.ToString(_formats[0], CultureInfo.InvariantCulture));
            }
        }

        private class NullableConverter : JsonConverter<DateTime?>
        {
            private readonly string[] _formats;

            public NullableConverter(string[] formats) => _formats = formats;

            public override DateTime? Read(ref Utf8JsonReader reader, Type _, JsonSerializerOptions __)
            {
                if (reader.TokenType != JsonTokenType.String) return reader.GetDateTime();

                var str = reader.GetString();
                var success = DateTime.TryParseExact(str, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt);

                var result = success ? dt : DateTime.Parse(str, CultureInfo.InvariantCulture);

                return result;
            }

            public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions __)
            {
                if (value.HasValue)
                {
                    writer.WriteStringValue(value.Value.ToString(_formats[0], CultureInfo.InvariantCulture));
                }
            }
        }
    }
}