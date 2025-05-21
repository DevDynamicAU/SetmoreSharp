namespace SetmoreSharp.Models
{
    public class TimeSlot
    {
        public string SlotTimeValue { get; set; }

        public TimeSpan? SlotTime => SlotTimeValue.HasValue() ? SlotTimeValue.ToTimeSpan() : null;
    }
}