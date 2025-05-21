using SetmoreSharp;

namespace Setmore.Tester.Tests.TimeSlot
{
    public static partial class TimeSlotTests
    {
        public static async Task GetAvailableTimeSlotsAsync(SetmoreClient client)
        {
            Program.WriteLine("BEGIN GetAvailableTimeSlotsAsync");

            try
            {
                var staffs = await client.GetStaffAsync();
                var staff = staffs?.FirstOrDefault(x => x.FirstName.ToLower() == "veronica");

                if (staff == null)
                {
                    Program.WriteLine("No staff found.");
                    return;
                }

                var services = await client.GetServicesAsync();

                foreach (var service in services)
                {
                    var resp = await client.GetAvailableTimeSlotsAsync(staffId: staff.Id, serviceId: service.Id, selectedDate: DateTime.Now.AddDays(1));

                    Program.WriteLine($"{resp?.Count() ?? 0} records found");
                }

                //Program.CheckResponse(resp, "GetAllServicesAsync", "Id", "found");
            }
            catch (Exception ex)
            {
                Program.WriteLine($"Error: {ex}");
            }

            Program.WriteLine("END GetStaffAsync");
        }
    }
}