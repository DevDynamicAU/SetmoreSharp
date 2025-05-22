using SetmoreSharp.Models;
using System.Text.Json.Serialization;

namespace SetmoreSharp
{
    public class CustomerDto
    {
        [JsonPropertyName("customer")]
        public List<Customer> Customers { get; set; }
    }
}