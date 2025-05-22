using SetmoreSharp;

namespace Setmore.Tester.Tests.Customer
{
    public static partial class CustomerTests
    {
        public static async Task CreateCustomerAsync(SetmoreClient client)
        {
            Program.WriteLine("BEGIN CreateCustomerAsync");

            try
            {
                var resp = await client.CreateCustomerAsync(firstName: "Peter", lastName: "test", email: "peter+test@devdynamic.com.au");

                Program.WriteLine($"{resp?.Id ?? ""} record found");
                //Program.CheckResponse(resp, "CreateCustomerAsync", "Id", "found");
            }
            catch (Exception ex)
            {
                Program.WriteLine($"Error: {ex}");
            }

            Program.WriteLine("END CreateCustomerAsync");
        }
    }
}