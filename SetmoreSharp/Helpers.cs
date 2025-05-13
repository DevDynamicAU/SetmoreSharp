using SetmoreSharp.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public static class Helpers
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions

        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new DoubleConverter(),
                               new DateTimeConverterFactory() }
        };
    }
}