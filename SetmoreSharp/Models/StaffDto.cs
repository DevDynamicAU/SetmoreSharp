using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public class StaffDto
    {
        [JsonPropertyName("cursor")]
        public string Cursor { get; set; }

        [JsonPropertyName("staffs")]
        public List<Staff> Staff { get; set; }
    }
}