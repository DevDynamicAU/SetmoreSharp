using SetmoreSharp;

namespace Setmore.Tester.Tests.Staff
{
    public static partial class StaffTests
    {
        public static async Task GetStaffAsync(SetmoreClient client)
        {
            Program.WriteLine("BEGIN GetStaffAsync");

            try
            {
                var resp = await client.GetStaffAsync();

                Program.WriteLine($"{resp?.Count() ?? 0} records found");
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