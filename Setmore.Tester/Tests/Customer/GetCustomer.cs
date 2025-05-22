using SetmoreSharp;

namespace Setmore.Tester.Tests.Customer
{
    public static partial class CustomerTests
    {
        public static async Task GetCustomerAsync(SetmoreClient client)
        {
            Program.WriteLine("BEGIN GetCustomerAsync");

            try
            {
                var resp = await client.GetCustomerAsync(firstName: "Hannah", lastName: "mealand");

                Program.WriteLine($"{resp?.Count() ?? 0} records found");
                //Program.CheckResponse(resp, "GetAllServicesAsync", "Id", "found");
            }
            catch (Exception ex)
            {
                Program.WriteLine($"Error: {ex}");
            }

            Program.WriteLine("END GetCustomerAsync");
        }
    }
}