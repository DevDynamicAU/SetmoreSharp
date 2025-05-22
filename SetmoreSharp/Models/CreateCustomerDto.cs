using SetmoreSharp.Models;
using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public class CreateCustomerDto
    {
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }
    }
}