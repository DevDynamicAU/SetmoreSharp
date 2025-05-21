using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public class Staff
    {
        [JsonIgnore]
        public string Id => Key;

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email_id")]
        public string Email { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("work_phone")]
        public string WorkPhone { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
}