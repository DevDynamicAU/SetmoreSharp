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

        /// <summary>
        /// If you ask for ApiResponse&lt;SetmoreToken&gt;, we'll extract the nested "token" object.
        /// Otherwise we just JsonSerializer.Deserialize into TResponse directly.
        /// </summary>
        public static TResponse DeserialiseResponse<TResponse>(string json, JsonSerializerOptions options) where TResponse : class
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var respType = typeof(TResponse);

            // detect ApiResponse<SetmoreToken> case
            var isApiResponse = respType.IsGenericType && respType.GetGenericTypeDefinition() == typeof(ApiResponse<>);

            if (isApiResponse && respType.GetGenericArguments()[0] == typeof(SetmoreToken))
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var responseNode = root.GetProperty("response").GetBoolean();
                var dataElem = root.GetProperty("data");

                // drill into "token"
                if (!dataElem.TryGetProperty("token", out var tokenElem)) throw new JsonException("Expected 'data.token'");

                var token = JsonSerializer.Deserialize<SetmoreToken>(tokenElem.GetRawText(), options) ?? throw new JsonException("Error deserialising SetmoreToken");

                var apiResp = new ApiResponse<SetmoreToken>
                {
                    Response = responseNode,
                    Data = token
                };

                // safe cast because we know TResponse == ApiResponse<SetmoreToken>
                return apiResp as TResponse ?? throw new InvalidCastException("Cannot cast to TResponse");
            }

            // fallback: user-provided TResponse must match the JSON shape
            return JsonSerializer.Deserialize<TResponse>(json, options)
                   ?? throw new JsonException($"Unable to deserialize into {respType.Name}");
        }
    }
}