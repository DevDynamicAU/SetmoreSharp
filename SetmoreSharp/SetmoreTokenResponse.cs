using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public record SetmoreTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; init; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; init; }

        [JsonPropertyName("user_id")]
        public string UserId { get; init; }

        [JsonIgnore]
        public string RefreshToken { get; init; }

        public string Error { get; set; }

        public bool IsError => !string.IsNullOrEmpty(Error);
    }
}