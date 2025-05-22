using System.Text.Json.Serialization;

namespace SetmoreSharp.Models
{
    public class CustomerAdditionalFields
    {
        [JsonPropertyName("serial_no")]
        public string SerialNo { get; set; }
    }
}