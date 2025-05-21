using SetmoreSharp.Models;

namespace SetmoreSharp
{
    public partial class SetmoreClient : ApiClient
    {
        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync(string staffId,
                                                                         string serviceId,
                                                                         DateTime selectedDate,
                                                                         bool? offHours = null,
                                                                         bool? doubleBooking = null,
                                                                         int? slotLimit = null,
                                                                         TimeZoneInfo timeZone = null)
        {
            var request = new Request();

            request.EndPoint = "slots";

            var tzName = timeZone?.Id ?? "Australia/Brisbane";

            var arguments = new TimeSlotArguments()
            {
                StaffKey = staffId,
                ServiceKey = serviceId,
                SelectedDate = selectedDate.ToString("dd/MM/yyyy"),
                OffHours = offHours,
                DoubleBooking = doubleBooking,
                SlotLimit = slotLimit,
                TimeZone = tzName,
            };

            var body = new ApiBody<TimeSlotArguments>(arguments);

            var resp = await PostAsync<TimeSlotDto, TimeSlotArguments>(request, body);

            var result = resp != null
                            ? resp.Slots.Select(x => new TimeSlot() { SlotTimeValue = x }).ToList()
                            : new List<TimeSlot>();

            return result;
        }
    }
}