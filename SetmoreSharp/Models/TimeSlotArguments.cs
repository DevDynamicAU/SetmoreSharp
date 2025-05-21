using System.Text.Json.Serialization;

namespace SetmoreSharp.Models
{
    public class TimeSlotArguments
    {
        [JsonPropertyName("staff_key")]
        public string StaffKey { get; set; }

        [JsonPropertyName("service_key")]
        public string ServiceKey { get; set; }

        [JsonPropertyName("selected_date")]
        public string SelectedDate { get; set; }

        [JsonPropertyName("off_hours")]
        public bool? OffHours { get; set; }

        [JsonPropertyName("double_booking")]
        public bool? DoubleBooking { get; set; }

        [JsonPropertyName("slot_limit")]
        public int? SlotLimit { get; set; }

        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; }
    }
}