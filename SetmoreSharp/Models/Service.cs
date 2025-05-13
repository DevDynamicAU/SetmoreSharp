using System.Text.Json.Serialization;

namespace SetmoreSharp.Models
{
    public class Service
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("service_name")]
        public string ServiceName { get; set; }

        [JsonPropertyName("staff_keys")]
        public IEnumerable<string> StaffKeys { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("buffer_duration")]
        public int? BufferDuration { get; set; }

        [JsonPropertyName("cost")]
        public double Cost { get; set; }

        [JsonPropertyName("currency")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}