using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public class TimeSlotDto
    {
        [JsonPropertyName("slots")]
        public List<string> Slots { get; set; }
    }
}