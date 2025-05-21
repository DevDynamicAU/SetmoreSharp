using System.Text.Json.Serialization;

namespace SetmoreSharp.Models
{
    public class ServiceCategory
    {
        [JsonIgnore]
        public string Id => Key;

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }

        [JsonPropertyName("serviceIdList")]
        public IEnumerable<string> ServiceIdList { get; set; }
    }
}