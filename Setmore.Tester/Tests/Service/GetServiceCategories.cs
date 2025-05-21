using SetmoreSharp;

namespace Setmore.Tester.Tests.Service
{
    public static partial class ServiceTests
    {
        public static async Task GetServiceCategoriesAsync(SetmoreClient client)
        {
            Program.WriteLine("BEGIN GetServiceCategoriesAsync");

            try
            {
                var resp = await client.GetServiceCategoriesAsync();

                Program.WriteLine($"{resp?.Count() ?? 0} records found");
                //Program.CheckResponse(resp, "GetServiceCategoriesAsync", "Id", "found");
            }
            catch (Exception ex)
            {
                Program.WriteLine($"Error: {ex}");
            }

            Program.WriteLine("END GetServiceCategoriesAsync");
        }
    }
}