using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public class ApiResponse<T> where T : class
    {
        [JsonPropertyName("response")]
        public bool Response { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}