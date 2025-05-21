using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public record SetmoreToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; init; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; init; }

        [JsonPropertyName("user_id")]
        public string UserId { get; init; }

        // <-- new raw epoch‐ms field
        [JsonPropertyName("expires")]
        public long ExpiresEpoch { get; init; }

        [JsonIgnore]
        public DateTime ExpiresAt => DateTimeOffset.FromUnixTimeMilliseconds(ExpiresEpoch).UtcDateTime;

        [JsonIgnore]
        public string RefreshToken { get; init; }

        public string Error { get; set; }

        public bool IsError => !string.IsNullOrEmpty(Error);
    }
}