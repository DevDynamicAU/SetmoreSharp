using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SetmoreSharp.Models
{
    public record Appointment
    {
        [JsonPropertyName("booking_id")]
        public string BookingId { get; init; }

        [JsonPropertyName("staff_key")]
        public string StaffKey { get; init; }

        [JsonPropertyName("service_key")]
        public string ServiceKey { get; init; }

        [JsonPropertyName("customer_key")]
        public string CustomerKey { get; init; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; init; }

        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; init; }

        [JsonPropertyName("status")]
        public string Status { get; init; }
    }
}